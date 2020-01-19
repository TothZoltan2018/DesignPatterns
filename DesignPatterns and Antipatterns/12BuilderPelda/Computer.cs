using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;

namespace _12BuilderPelda
{
    public class Computer
    {
        [JsonConverter(typeof(StringEnumConverter))] //Ez kell, hogy ne az enum szamerteket, hanem a nevet jelenitsuk meg
        public Processor Processor { get; set; }

        [JsonConverter(typeof(StringEnumConverter))] //Ez kell, hogy ne az enum szamerteket, hanem a nevet jelenitsuk meg
        public OS OS { get; set; }
        public int HDD { get; set; }
        public bool HasDVD { get; set; }
        public bool HasSoundCard { get; set; }
        public bool HasUSB { get; set; }
        public List<string> Applications { get; set; }

        public void Display()
        {
            //Json-os serializaciot csinajunk a sok property miatt ez egyszerubb lesz
            Console.WriteLine(JsonConvert.SerializeObject(this, Formatting.Indented));
        }
    }
}