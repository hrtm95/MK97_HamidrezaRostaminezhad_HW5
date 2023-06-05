using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Database
{
    public static class DbContext<T> where T : class
    {
        public static bool WriteJson(List<T> list, string path)
        {
            try
            {
                var Text = JsonConvert.SerializeObject(list);
                File.WriteAllText(path, Text);
                return true;
            }
            catch
            {
                //for any exaption we can throwexeption?? whats beter?
                return false;
            }
        }
        public static List<T>? ReadJson(string path)
        {
            try
            {
                string text = File.ReadAllText(path);
                var list = JsonConvert.DeserializeObject<List<T>>(text);
                return list;
            }
            catch
            {
                //for any exaption we can throw exeption?? whats beter?
                return null;
            }
        }
 
    }
}
