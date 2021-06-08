using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Black_White
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void openButton_Click(object sender, EventArgs e)
        {
            //диалог для выбора файла
            OpenFileDialog ofd = new OpenFileDialog();

            //фильтр форматов файлов
            //ofd.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
            ofd.Filter = "Image Files(*.BMP)|*.BMP|All files (*.*)|*.*";

            //если в диалоге была нажата кнопка ОК
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    //загрузка изображения
                    pictureBox1.Image = new Bitmap(ofd.FileName);
                }
                catch
                {
                    //в случае ошибки выводим MessageBox
                    MessageBox.Show("Невозможно открыть выбранный файл", "Ошибка",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveButton_Click(object sender, EventArgs e)
        {
            if (pictureBox2.Image != null) //если имеется изображение в pictureBox2
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Title = "Сохранить картинку как...";
                sfd.OverwritePrompt = true; //показать ли "Перезаписать файл", если файл с таким именем уже имеется в каталоге
                sfd.CheckPathExists = true; //показать ли диалоговое окно, если указывается не существующий путь

                //фильтр форматов файлов
                //sfd.Filter = "Image Files(*.BMP;*.JPG;*.GIF;*.PNG)|*.BMP;*.JPG;*.GIF;*.PNG|All files (*.*)|*.*";
                sfd.Filter = "Image Files(*.BMP)|*.BMP|All files (*.*)|*.*";
                sfd.ShowHelp = true; //отображается ли кнопка Справка в диалоговом окне, если в диалоге была нажата кнопка ОК
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        //сохранение изображения
                        pictureBox2.Image.Save(sfd.FileName);
                    }
                    catch
                    {
                        //в случае ошибки выводим MessageBox
                        MessageBox.Show("Невозможно открыть изображение", "Ошибка",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void transformButton_Click(object sender, EventArgs e)
        {
            if (pictureBox1.Image != null)
            {
                Bitmap input = new Bitmap(pictureBox1.Image);
                Bitmap output = new Bitmap(input.Width, input.Height);

                for (int j = 0; j < input.Height; j++)
                    for (int i = 0; i < input.Width; i++)
                    {
                        UInt32 pixel = (UInt32)(input.GetPixel(i, j).ToArgb());

                        float R = (float)((pixel & 0x00FF0000) >> 16);
                        float G = (float)((pixel & 0x0000FF00) >> 8);
                        float B = (float)(pixel & 0x000000FF);

                        R = G = B = (R + G + B) / 3.0f;

                        UInt32 newPixel = 0xFF000000 | ((UInt32)R << 16) | ((UInt32)G << 8) | ((UInt32)B);

                        output.SetPixel(i, j, Color.FromArgb((int)newPixel));
                    }

                pictureBox2.Image = output;
            }
        }
    }
}
