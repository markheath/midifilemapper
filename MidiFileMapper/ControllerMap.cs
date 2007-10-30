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
            controllerMap.name = XmlUtils.GetAttribute(mappingNode,"Name","");
            controllerMap.inControllers = new InputValueParameters(XmlUtils.GetAttribute(mappingNode,"InController","*"));
            controllerMap.inChannels = new InputValueParameters(XmlUtils.GetAttribute(mappingNode,"InChannel","*"));
            controllerMap.inValues = new InputValueParameters(XmlUtils.GetAttribute(mappingNode,"InValue","*"));
            controllerMap.outController = new NoteEventOutputParameters(XmlUtils.GetAttribute(mappingNode,"OutController","*"), 0, 127);
            controllerMap.outChannel = new NoteEventOutputParameters(XmlUtils.GetAttribute(mappingNode,"OutChannel","*"), 1, 16);
            controllerMap.outValue = new NoteEventOutputParameters(XmlUtils.GetAttribute(mappingNode,"OutValue","*"), 0, 127);
            return controllerMap;
        }


        public bool Apply(MidiEvent inEvent, EventRuleArgs args)
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
