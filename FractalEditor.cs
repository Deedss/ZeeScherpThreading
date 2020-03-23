using System;
using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;
using System.Runtime.Serialization;

namespace ZeeScherpThreading
{
    /// <summary>
    /// Creates new fractal templates.
    /// Saves fractal templates to a list.
    /// Saves and loads template lists to and from Json files.
    /// </summary>
    class FractalEditor
    {
        private List<FractalTemplate.FractalTemplate> fractals;

        public FractalEditor()
        {

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="filename"></param>
        public void Edit(string filename)
        {
            // Gets the string from a specific Json file
            string fileName = filename + ".json";
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"SavedFarctals\" + fileName;
            string json = File.ReadAllText(filePath);
        }

        /// <summary>
        /// Gets abstract list of all the fractal templates
        /// </summary>
        /// <returns> A list of the existing fractal templates </returns>
        public List<FractalTemplate.FractalTemplate> GetFractals()
        {
            return this.fractals;
        }

        /// <summary>
        /// Put the fractal template list in a Json file
        /// </summary>
        /// <param name="filename"> The name of the Json file </param>
        public void SaveFractals(string filename) // voorbeeld hoe je Json file saved 
        {
            //TODO Serialize the list to Json
            string json = SerializeJson();            // The string to put in the json file

            string fileName = filename + ".json";
            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"SavedFarctals\" + fileName, json);
        }

        /// <summary>
        /// Get a fractal template list from a Json file and add it to the template list
        /// </summary>
        /// <param name="filename"> The name of the Json file to retrieve fractal templates from </param>
        public void LoadFractals(string filename)
        {
            // Gets a string of the contents of the Json file
            string fileName = filename + ".json";
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"SavedFarctals\" + fileName;
            string json = File.ReadAllText(filePath);

            // Deserialize the string
            DeserializeJson(json);
        }

        /// <summary>
        /// Serializes the the fractal templates to insert into a Json file
        /// </summary>
        /// <returns> The serialized string </returns>
        public string SerializeJson()
        {
            // Create a new class to serialize
            var toserialize = new ClassToSerializeViaJson();
            var binder = new TypeNameSerializationBinder("ConsoleApplication.{0}, ConsoleApplication");

            toserialize.CollectionToSerialize = fractals;

            string json = JsonConvert.SerializeObject(toserialize, Formatting.Indented,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Binder = binder
                });

            return json;
        }

        /// <summary>
        /// Deserialize the information from the string of a Json file
        /// </summary>
        /// <param name="json"> The Json string to deserialize </param>
        public void DeserializeJson(string json)
        {
            var binder = new TypeNameSerializationBinder("ConsoleApplication.{0}, ConsoleApplication");

            var obj = JsonConvert.DeserializeObject<ClassToSerializeViaJson>(json,
                new JsonSerializerSettings
                {
                    TypeNameHandling = TypeNameHandling.Auto,
                    Binder = binder
                });

            this.fractals = obj.CollectionToSerialize;
        }

    }

    /// <summary>
    /// A class to serialize the list fractals from FractalEditor
    /// </summary>
    class ClassToSerializeViaJson
    {
        public ClassToSerializeViaJson()
        {
            this.CollectionToSerialize = new List<FractalTemplate.FractalTemplate>();
        }
        public List<FractalTemplate.FractalTemplate> CollectionToSerialize { get; set; }
    }

    public class TypeNameSerializationBinder : SerializationBinder
    {
        public string TypeFormat { get; private set; }

        public TypeNameSerializationBinder(string typeFormat)
        {
            TypeFormat = typeFormat;
        }

        public override void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }

        public override Type BindToType(string assemblyName, string typeName)
        {
            var resolvedTypeName = string.Format(TypeFormat, typeName);
            return Type.GetType(resolvedTypeName, true);
        }
    }
}
