using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CSMusicPlayer
{
    //se crea clase pueblica Song
    public class Song
    {
        //se le crean dos propiedades
        public string Name { get; set; }
        public string Path { get; set; }

        //metodo constructor sin parametros
        public Song()
        {

        }

        //metodo constructor con parametros 
        public Song(string name , string path)
        {
            Name = name;
            Path = path;
        }
    }
}
