using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CSMusicPlayer
{
    public partial class MusicPlayer : Form
    {
        //se crean dos variables locales fuera del metodo btnAdd_Click para contener tanto la ruta como los nombres de los archivos de mucica
        List<string> _sonNames; //nombres de los archivos
        List<string> _sonPaths; //lista con la ruta de los archivos

        public MusicPlayer()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            //la clase OpenFileDialog es la que muestra la ventana para seleccionar el archivo
            OpenFileDialog dialog = new OpenFileDialog();

            //la funcion Multiselect permiter seleccinar mas dun archivo 
            dialog.Multiselect = true;

            //despues de instanciar la clase OpenFileDialog utilizamos el metodo .ShowDialog() el cual retorna un DialogResult.OK si el usuario selecciona un archivo
            if (dialog.ShowDialog() == DialogResult.OK)
            {
                //dialog.SafeFileNames esta propiedad nos permite traer el nombre del archivo y la ruta donde se encuentra 
                //dialog.SafeFileName esta propiedad solo nos trae el nombre del archivo
                //con el metodo ToList() convertimos un array a List
                //_sonNames = dialog.SafeFileNames.ToList();
                //_sonPaths = dialog.FileNames.ToList();

                AddSongsToList(dialog.SafeFileNames.ToList() ,dialog.FileNames.ToList());
            }
        }

        //secrea un metodo que agregue los archivos a la lista de reproduccion
        //recibe como parametros las listas de names y ruta de archivos 
        private void AddSongsToList(List<string> names , List<string> paths)
        {
            //se crea un if para inicializar la lista _sonNames que esta sin inicializar
            if (_sonNames == null)
                _sonNames = new List<string>();

            if (_sonPaths == null)
                _sonPaths = new List<string>();

            foreach (var item in names)
            {
                //si la cancion existe o no 
                if (!ExistsOnList(item))
                {
                    _sonNames.Add(item);
                   _sonPaths.Add(GetPath(item,paths));
                }
            }

            //agregar las canciones al ListsBox 
            songsList.DataSource = _sonNames;

            RefreshList();
        }


        //se crea el metodo ExistsOnList para saber si existe o no el nombre de la cancion
        private bool ExistsOnList(string song)
        {
            bool exist = false;

            foreach (var item in _sonNames)
            {
                if (item == song)
                
                    exist = true;
            }

            return exist;
        }

        //se crea un nuevo metodo actualiza y contiene las rutas de las canciones
        private string GetPath(string FileName , List<string> songsPath = null)
        {
            string actualPath = string.Empty;

            if (songsPath == null)
                songsPath = _sonPaths;

            foreach (var path in songsPath)
            {
                if (path.Contains(FileName))
                    actualPath = path;
            }
            return actualPath;
        }

        private void songsList_DoubleClick(object sender, EventArgs e)
        {
            string a = songsList.Text;
            axWindowsMediaPlayer1.URL = GetPath(songsList.Text);
        }

        //se crea el evnto click para el boton remover cancion
        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (_sonNames != null)
                _sonNames.Remove(songsList.Text);

            if (_sonPaths != null)
                _sonPaths.Remove(GetPath(songsList.Text));

            RefreshList();
        }


        //se crea un metodo para refrescar la lista de reproduccion
        private void RefreshList()
        {
            songsList.DataSource = null;
            songsList.DataSource = _sonNames;
        }
    }
}
