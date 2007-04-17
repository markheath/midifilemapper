using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using NAudio.Midi;
using NAudio.Utils;
using MarkHeath.MidiUtils.Properties;

namespace MarkHeath.MidiUtils
{
    public class MidiFileMapper
    {
        List<string> filesToConvert;
        MidiMappingRules mappingRules;
        Settings settings;
        int errors;
        int directoriesCreated;
        int filesConverted;
        int filesCopied;
        
        public event EventHandler<ProgressEventArgs> Progress;

        public MidiFileMapper(MidiMappingRules mappingRules)
            : this(mappingRules, null)
        {
        }

        public MidiFileMapper(MidiMappingRules mappingRules, List<string> filesToConvert)
        {
            this.filesToConvert = filesToConvert;
            this.mappingRules = mappingRules;
            this.settings = Settings.Default;
        }

        public void Start()
        {
            DateTime start = DateTime.Now;
            LogInformation("Started at {0}", DateTime.Now);
            errors = 0;
            directoriesCreated = 0;
            filesConverted = 0;
            filesCopied = 0;
        
            if (settings.UseInputFolder)
            {
                ProcessFolder(settings.InputFolder,settings.OutputFolder);
            }
            else
            {
                foreach (string fileName in filesToConvert)
                {
                    ProcessFile(fileName, settings.OutputFolder);
                }
            }
            TimeSpan elapsed = DateTime.Now - start;
            LogInformation("Finished in {0}", elapsed);
        }

        private void ProcessFolder(string folder, string outputFolder)
        {
            string[] midiFiles = Directory.GetFiles(folder);
            foreach (string midiFile in midiFiles)
            {
                try
                {
                    ProcessFile(midiFile, outputFolder);
                }
                catch (Exception e)
                {
                    LogError("Unexpected error processing file {0}", midiFile);
                    LogError(e.ToString());
                    errors++;
                }
            }

            string[] subfolders = Directory.GetDirectories(folder);
            foreach (string subfolder in subfolders)
            {
                string folderName = Path.GetFileName(subfolder);
                string newOutputFolder = Path.Combine(outputFolder, folderName);

                if (!Directory.Exists(newOutputFolder))
                {
                    //if (settings.VerboseOutput)
                    {
                        LogTrace("Creating folder {0}", newOutputFolder);
                    }
                    Directory.CreateDirectory(newOutputFolder);
                    directoriesCreated++;
                }

                ProcessFolder(subfolder, newOutputFolder);
            }
        }


        private void ProcessFile(string sourceFile, string outputFolder)
        {
            string outputFile = Path.Combine(outputFolder, Path.GetFileName(sourceFile));
                
            if (sourceFile.ToLower().EndsWith(".mid"))
            {
                LogInformation("Converting {0}", sourceFile);

                if (mappingRules.ConvertFile(sourceFile, outputFile, settings.OutputFileType))
                {
                    filesConverted++;
                }
                else
                {
                    LogWarning("All notes filtered out for {0}", sourceFile);
                }
            }
            else
            {
                if (settings.UseInputFolder && settings.CopyNonMidi)
                {
                    File.Copy(sourceFile, outputFile);
                    filesCopied++;
                }
            }
        }

        private void LogTrace(string message, params object[] args)
        {
            OnProgress(this, new ProgressEventArgs(ProgressMessageType.Trace,
                message, args));
        }

        private void LogInformation(string message, params object[] args)
        {
            OnProgress(this, new ProgressEventArgs(ProgressMessageType.Information,
                message, args));
        }

        private void LogWarning(string message, params object[] args)
        {
            OnProgress(this, new ProgressEventArgs(ProgressMessageType.Warning,
                message, args));
        }

        private void LogError(string message, params object[] args)
        {
            OnProgress(this, new ProgressEventArgs(ProgressMessageType.Error,
                message, args));
        }

        protected void OnProgress(object sender, ProgressEventArgs args)
        {
            if (Progress != null)
            {
                Progress(sender, args);
            }
        }
    }
}
