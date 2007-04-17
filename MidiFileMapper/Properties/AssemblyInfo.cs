using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;

// General Information about an assembly is controlled through the following 
// set of attributes. Change these attribute values to modify the information
// associated with an assembly.
[assembly: AssemblyTitle("MIDI File Mapper")]
[assembly: AssemblyDescription("MIDI File Event Mapping Utility")]
[assembly: AssemblyConfiguration("")]
[assembly: AssemblyCompany("Mark Heath")]
[assembly: AssemblyProduct("MIDI File Mapper")]
[assembly: AssemblyCopyright("Copyright © Mark Heath 2007")]
[assembly: AssemblyTrademark("")]
[assembly: AssemblyCulture("")]

// Setting ComVisible to false makes the types in this assembly not visible 
// to COM components.  If you need to access a type in this assembly from 
// COM, set the ComVisible attribute to true on that type.
[assembly: ComVisible(false)]

// The following GUID is for the ID of the typelib if this project is exposed to COM
[assembly: Guid("ccf3dcf9-c0ef-4a9f-ab31-959c3097797a")]

// Version information for an assembly consists of the following four values:
//
//      Major Version
//      Minor Version 
//      Build Number
//      Revision
//
[assembly: AssemblyVersion("0.1.20.0")]
[assembly: AssemblyFileVersion("0.1.20.0")]

// build 1 - 14 Oct 2006
// initial wizard style UI design
// build 2 - 16 Oct 2006
// reads XML rules file
// build 3 - 17 Oct 2006
// finds SONAR drum maps
// improved GUI to look more wizard-like
// added a basic installer script
// build 4 - 18 Oct 2006
// more work on select mapping & options pages
// GM to EZD map added
// about box
// build 5 - 19 Oct 2006
// better input range handling
// turns SONAR drum map entries into MFM entries
// build 6 - 25 Oct 2006
// a MIDI file mapper class
// threading & logging functionality
// build 7 - 26 Oct 2006
// finished design work on rules xml file
// ability to upgrade the settings
// remembers last selected file
// basic support for reading Cubase DRM files
// build 8 - 27 Oct 2006
// exclude rules added
// build 9 - 3 Nov 2006
// more work on installer
// makes use of some of the generic NAudio.Utils classes
// begun a DFH1 to DFH EZX Conversion
// ControllerMap support
// Generic IEventRule
// Rework of user interface to allow specification of an input folder
// build 10 - 6 Nov 2006
// Bugfix folder page
// Aftertouch map
// Pitchwheel map
// Insert rule
// ExcludeRule.Apply
// ControllerMap.Apply
// build 11 - 7 Nov 2006
// AfterTouchMap.Apply
// NoteMap.Apply
// InsertRule.Apply
// build 12 - 9 Nov 2006
// PitchWheelMap.Apply
// Added recursive folder processing code
// Support for inserting events
// Creation of the new MIDI file
// Copying non-MIDI files
// build 13 - 10 Nov 2006
// support for filtering out unmapped notes
// experimenting with Visual Studio Deployment
// build 14 - 24 Nov 2006
// input folder page validation
// build 15 - 14 Dec 2006
// select mapping page now uses a listbox instead of a combo box
// build 16 - 3 Jan 2007
// minor updates
// build 17 - 5 Jan 2007
// GM to Latin Percussion EZX
// Rudimentary support for a percent in the note value
// Does not copy a file if all notes filtered out
// GM to AD
// build 18 - 29 Mar 2007
// added to codeplex
// improved error handling on map load failures
// fixed various bugs
// build 19 - 30 Mar 2007
// adding nunit support
// some reworking of event processing logic
// build 20 - 2 Apr 2007
// modified syntax of the General setting of mapping file
// file conversion moved into mapping rules
// some automated tests based on files
// fixed some bugs in thanks to unit tests:
//     - NAudio sysex export bug
//     - NoteMap not mapping Off Event properties
// build 21 - 3 Apr 2007
// more work on CodePlex help pages
// more automated tests
// updated to latest NAudio with stable sort of MIDI events
// attempt at EZD to AD

// - refactor mapping rules conversion function to deal with lists of events only
// - group rules into type again (controller, note, etc)
// - for each type, unmatched events can be passed or excluded
// - import: check for required parameters in XML
// - warn about unexpected attributes / nodes in XML?
// - ability to report XML line number
// - finish off help file
// - finish off installer
// - more automated tests
// - conversion from type 0 to type 1 should be much more clever - a track per channel event

// For a future version
// - Mapping Rules selection page needs some work - an additional info dialog,
//      plus ability to show previous map if loaded from custom location
// - consider an interface for cancelling
// - NoteMap: add support for velocity scaling
// - NoteMap: add support for note names
// - EndTrack - last event, latest event across all tracks, round to next beat, round to next measure
// - Unique file name creation
// - NAudio support RIFF MIDI files
// - NAudio support follow-on events
// - exporting rule files (make a convert tool)
// - find Cubase drum maps
// - Options dialog - choose map folders
// - support maps from other sequencers
// - design something to allow track naming
// - command line mode
// - exclude all rule support
// - details view on select mappings page that shows information about the selected file
// - allow note offs turning into note on

// Lots of maps
// - EZD to GM
// - GM to EZX Cocktail
// - EZX Cocktail to GM
// - DFH1 to EZX DFH


// Rules are:
// 1. All notes are excluded by default
// 2. All other events are included by default
// 3. The NoteMap rule remaps notes
// 4. The Exclude rule deletes events
// 5. ControllerMap, AfterTouchMap and PitchWheelMap really just for changing channel

// don't give access to:
// <Insert Type="Copyright" Time=0 Value="Blah blah blah" />
// EndTrack, KeySignature, TimeSignature, Tempo
// <Exclude EventType="Controller" Controller="20" Value="0-12"/>
// <Exclude EventType="PitchWheel" />
// <Exclude EventType="PatchChange" />
// <Exclude EventType="AfterTouch" Type="Channel" />
// <Exclude EventType="Marker" />
// <Exclude EventType="SequencerSpecific" />
// <Exclude EventType="NonStandard" />
// SmpteOffset, CuePoint, Lyric, DeviceName, ProgramName, TrackInstrumentName, SequenceTrackName, TrackSequenceNumber, Copyright
// channel events:
// <Exclude EventType="Sysex" />
// Eox, KeyAfterTouch, ContinueSequence, StopSequence, StartSequence, TimingClock
// ChannelAfterTouch, PatchChange

// <Insert EventType="Marker" Track="1" 
// <NoteMap InNote="12" OutNote="12" OutChannel="1" OutVelocity="+10" OutDuration="1" />
// <ControllerMap InController="21" OutController="21" OutValue="+5" />
// <ChannelEventMap OutChannel="5" />