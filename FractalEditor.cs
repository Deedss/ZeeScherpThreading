using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Numerics;
using System.IO;
using System.Text.Json;
using System.Text.Json.Serialization;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
        /// Create a new fractal template based on an existing template and add this new template to the list of existing templates
        /// </summary>
        /// <param name="fractal"> The fractal template where the new template will be based on </param>
        /// <param name="a"> First complex parameter of the fractal </param>
        /// <param name="b"> Second complex parameter of the fractal </param>
        public void create(FractalTemplate.FractalTemplate fractal, Complex a, Complex b)
        {
            // TODO Fix the fractal
            fractal newFractalTemplate = new fractal(a, b);
            fractals.Add(newFractalTemplate);

        }

        /// <summary>
        /// Gets abstract list of all the fractal templates
        /// </summary>
        /// <returns> A list of the existing fractal templates </returns>
        public List<FractalTemplate.FractalTemplate> getFractals()
        {
            return this.fractals;
        }

        /// <summary>
        /// Put the fractal template list in a Json file
        /// </summary>
        /// <param name="filename"> The name of the Json file </param>
        public void saveFractals(String filename) // voorbeeld hoe je Json file saved 
        {
            //TODO Serialize the list to Json
            
            JObject fractalJson = new JObject(new JProperty("Fractal Templates", getFractals()));

            File.WriteAllText(AppDomain.CurrentDomain.BaseDirectory + @"SavedFarctals\" + filename + ".json", fractalJson.ToString());

            // write JSON directly to a file
            using (StreamWriter file = File.CreateText(AppDomain.CurrentDomain.BaseDirectory + @"SavedFarctals\" + filename + ".json"))
            using (JsonTextWriter writer = new JsonTextWriter(file))
            {
                fractalJson.WriteTo(writer);
            }
            
        }

        /// <summary>
        /// Get a fractal template list from a Json file and add it to the template list
        /// </summary>
        /// <param name="filename"> The name of the Json file to retrieve fractal templates from </param>
        public void loadFractals(String filename)
        {
            //TODO JSON with deserialization

            // Gets a string of the contents of the Json file
            string filePath = AppDomain.CurrentDomain.BaseDirectory + @"SavedFarctals\" + filename + ".json";
            string text = File.ReadAllText(filePath);


            // old
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Title = "Select A File";
            openDialog.Filter = "Json Files (*.Json)|*.Json" + "All Files (*.*)|*.*"; 
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                string file = openDialog.FileName;
            }
        }

        /// <summary>
        /// Serializes the the fractal templates to insert into a Json file
        /// </summary>
        public async Task serializeJsonAsync() 
        {
            // TODO Finish serialization
            using (FileStream fs = File.Create(fileName))
            {
                await JsonSerializer.SerializeAsync(fs, weatherForecast);
            }
        }

        /// <summary>
        /// Deserialize the information from a Json file
        /// </summary>
        public void deserializeJson()  
        {
            // TODO Finish deserialization
            var text = File.ReadAllText(myFileName);
            mydictionary = JsonConvert.DeserializeObject<Dictionary<string, string>>(text);

        }
    }
}
