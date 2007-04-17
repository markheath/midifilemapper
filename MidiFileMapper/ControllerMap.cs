using System;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using NAudio.Midi;

namespace MarkHeath.MidiUtils
{
    class ControllerMap : IEventRule
    {
        private string name;
        private InputValueParameters inControllers;
        private InputValueParameters inChannels;
        private InputValueParameters inValues;
        private NoteEventOutputParameters outController;
        private NoteEventOutputParameters outChannel;
        private NoteEventOutputParameters outValue;        

        public static ControllerMap LoadFromXmlNode(XmlNode mappingNode)
        {
            ControllerMap controllerMap = new ControllerMap();
            controllerMap.name = mappingNode.Attributes["Name"].Value ?? "";
            controllerMap.inControllers = new InputValueParameters(mappingNode.Attributes["InController"].Value ?? "*");
            controllerMap.inChannels = new InputValueParameters(mappingNode.Attributes["InChannel"].Value ?? "*");
            controllerMap.inValues = new InputValueParameters(mappingNode.Attributes["InValue"].Value ?? "*");
            controllerMap.outController = new NoteEventOutputParameters(mappingNode.Attributes["OutController"].Value ?? "*", 0, 127);
            controllerMap.outChannel = new NoteEventOutputParameters(mappingNode.Attributes["OutChannel"].Value ?? "*", 1, 16);
            controllerMap.outValue = new NoteEventOutputParameters(mappingNode.Attributes["OutValue"].Value ?? "*", 0, 127);
            return controllerMap;
        }


        public bool Apply(MidiEvent inEvent)
        {
            bool match = false;
            if (inEvent.CommandCode == MidiCommandCode.ControlChange)
            {
                ControlChangeEvent controlEvent = (ControlChangeEvent)inEvent;
                if (inControllers.IsValueIncluded((int)controlEvent.Controller)
                    && inChannels.IsValueIncluded(controlEvent.Channel)
                    && inValues.IsValueIncluded(controlEvent.ControllerValue))
                {
                    controlEvent.ControllerValue = outValue.ProcessValue(controlEvent.ControllerValue);
                    controlEvent.Controller = (MidiController) outController.ProcessValue((int)controlEvent.Controller);
                    controlEvent.Channel = outChannel.ProcessValue(controlEvent.Channel);
                    match = true;
                }
            }
            return match;
        }

    }
}
