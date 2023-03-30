using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Collections;


namespace Do_Min_v._2._0

{
    public partial class Form1 : Form
    {
        #region Biến toàn phần
        Image Img_cell = new Bitmap(Properties.Resources.img_cell, 25, 25);
        Image Img_bom = new Bitmap(Properties.Resources.img_bom, 25, 25);
        Image Img_bom_X = new Bitmap(Properties.Resources.img_bom_X, 25, 25);
        Image Img_co = new Bitmap(Properties.Resources.Img_co, 25, 25);
        Image Img_poit1 = new Bitmap(Properties.Resources.img_poit1, 25, 25);
        Image Img_poit2 = new Bitmap(Properties.Resources.img_poit2, 25, 25);
        Image Img_poit3 = new Bitmap(Properties.Resources.img_poit3, 25, 25);
        Image Img_poit4 = new Bitmap(Properties.Resources.img_poit4, 25, 25);
        Image Img_poit5 = new Bitmap(Properties.Resources.img_poit5, 25, 25);
        Image Img_poit6 = new Bitmap(Properties.Resources.img_poit6, 25, 25);
        Image Img_poit7 = new Bitmap(Properties.Resources.img_poit7, 25, 25);
        Image Img_poit8 = new Bitmap(Properties.Resources.img_poit8, 25, 25);
        Image Img_poit9 = new Bitmap(Properties.Resources.img_poit9, 25, 25);
        Stack Hangdoi = new Stack();
        int time;
        int  time2 = 60;
        int indextime = 1;
        int[,] mainboard ;

        int[,] co ;
        int[,] kiemtraoco; // đánh dấu các ô đã mở hay chưa
      
        Graphics grs;

        #endregion

        #region load From
        public Form1()
        {
            InitializeComponent();
            mainboard = new int[Convert.ToInt32(nudrow.Text.ToString()), Convert.ToInt32(nudcol.Text.ToString())];
            co = new int[Convert.ToInt32(nudrow.Text.ToString()), Convert.ToInt32(nudcol.Text.ToString())];
            kiemtraoco = new int[Convert.ToInt32(nudrow.Text.ToString()), Convert.ToInt32(nudcol.Text.ToString())];
            khoitaomangmoi(Convert.ToInt32(nudrow.Text.ToString()), Convert.ToInt32(nudcol.Text.ToString()), Convert.ToInt32(nudbom.Text.ToString()));
            khoitaomangco(Convert.ToInt32(nudrow.Text.ToString()), Convert.ToInt32(nudcol.Text.ToString()));
            grs = panel1.CreateGraphics();
        }
        #endregion
        // Khởi Tạo Giá Trị
        #region khởi tạo gán giá trị
        public void khoitaomangco(int _cot, int _dong)
        {
            for (int i = 0; i < _cot; i++)
            {
                for (int j = 0; j < _dong; j++)
                {
                    co[i, j] = -2;

                }
            }

            for (int i = 0; i < _cot; i++)
            {
                for (int j = 0; j < _dong; j++)
                {
                    kiemtraoco[i, j] = 0;

                }
            }
        }
        // Chạy khi new game
        public void khoitaomangmoi(int _cot, int _dong, int _bom)
        {
            // gán giá trị mặt định
            for (int i = 0; i < _cot; i++)
            {
                for (int j = 0; j < _dong; j++)
                {
                    mainboard[i, j] = 0;

                }
            }
            // đặt bom ngẫu nhiên

            Random cot = new Random();

            int icot = 0;
            int jdong;
            for (int i = 0; i < Convert.ToInt32(nudbom.Text.ToString()); )
            {


                icot = cot.Next(0,Convert.ToInt32(nudrow.Text.ToString()) - 1);
                jdong = cot.Next(0,Convert.ToInt32(nudcol.Text.ToString()) - 1);
                if (mainboard[icot, jdong] != -1)
                {
                    mainboard[icot, jdong] = -1; i++;
                }
            }
            // giá trị các ô xung quanh
            for (int i = 0; i < _cot; i++)
            {
                for (int j = 0; j < _dong; j++)
                {
                    if (mainboard[i, j] == -1)
                    {
                        if (j - 1 >= 0 && mainboard[i, j - 1] != -1) mainboard[i, j - 1] = mainboard[i, j - 1] + 1;
                        if (j + 1 < _dong && mainboard[i, j + 1] != -1) mainboard[i, j + 1] = mainboard[i, j + 1] + 1;

                        if (i - 1 >= 0)
                        {
                            if (mainboard[i - 1, j] != -1) mainboard[i - 1, j] = mainboard[i - 1, j] + 1;
                            if (j - 1 >= 0 && mainboard[i - 1, j - 1] != -1) mainboard[i - 1, j - 1] = mainboard[i - 1, j - 1] + 1;
                            if (j + 1 < _dong && mainboard[i - 1, j + 1] != -1) mainboard[i - 1, j + 1] = mainboard[i - 1, j + 1] + 1;
                        }

                        if (i + 1 < _cot)
                        {
                            if (mainboard[i + 1, j] != -1) mainboard[i + 1, j] = mainboard[i + 1, j] + 1;
                            if (j - 1 >= 0 && mainboard[i + 1, j - 1] != -1) mainboard[i + 1, j - 1] = mainboard[i + 1, j - 1] + 1;
                            if (j + 1 < _dong && mainboard[i + 1, j + 1] != -1) mainboard[i + 1, j + 1] = mainboard[i + 1, j + 1] + 1;
                        }
                    }
                }
            }
        }
        #endregion
        // mở ô tróng kiểu sida Biểu tượng cảm xúc squint 
        #region Mở ô tróng
        public void mootrong(int _cot, int _dong, Graphics g, int x, int y)
        {


            // ô ở trên
            if (_dong !=  Convert.ToInt32(nudcol.Text.ToString())-1 && co[_cot, _dong + 1] == -2)
            {
                if (mainboard[_cot, _dong + 1] == 0 && _dong != Convert.ToInt32(nudcol.Text.ToString()) && kiemtraoco[_cot, _dong + 1] == 0)
                {
                    kiemtraoco[_cot, _dong + 1] = 1;
                    grs.DrawImage(Img_poit9, (_cot) * 25, (_dong + 1) * 25);
                    Hangdoi.Push(_cot);
                    Hangdoi.Push(_dong + 1);
                }
                else
                {
                    moochuaso(_cot, _dong + 1, (_cot) * 25, (_dong + 1) * 25);
                }
            }
            // ô ở dưới
            if (_dong != 0 && co[_cot, _dong - 1] == -2)
            {
                if (mainboard[_cot, _dong - 1] == 0 && kiemtraoco[_cot, _dong - 1] == 0)
                {
                    grs.DrawImage(Img_poit9, _cot * 25, (_dong - 1) * 25);
                    Hangdoi.Push(_cot);
                    Hangdoi.Push(_dong - 1);
                    kiemtraoco[_cot, _dong - 1] = 1;
                }
                else
                {
                    moochuaso(_cot, _dong - 1, (_cot) * 25, (_dong - 1) * 25);
                }
            }
            // ô ở bên phải

            if (_cot != Convert.ToInt32(nudrow.Text.ToString())-1 && co[_cot + 1, _dong] == -2)
            {
                if (mainboard[_cot + 1, _dong] == 0 && kiemtraoco[_cot + 1, _dong] == 0)
                {
                    kiemtraoco[_cot + 1, _dong] = 1;
                    grs.DrawImage(Img_poit9, (_cot + 1) * 25, (_dong) * 25);
                    Hangdoi.Push(_cot + 1);
                    Hangdoi.Push(_dong);
                }
                else
                {
                    moochuaso(_cot + 1, _dong, (_cot + 1) * 25, (_dong) * 25);
                }
            }
            // ô bên trái
            if (_cot != 0 && co[_cot - 1, _dong] == -2)
            {
                if (mainboard[_cot - 1, _dong] == 0 && _cot != 0 && kiemtraoco[_cot - 1, _dong] == 0)
                {
                    kiemtraoco[_cot - 1, _dong] = 1;
                    grs.DrawImage(Img_poit9, (_cot - 1) * 25, (_dong) * 25);
                    Hangdoi.Push(_cot - 1);
                    Hangdoi.Push(_dong);
                }
                else
                {
                    moochuaso(_cot - 1, _dong, (_cot - 1) * 25, (_dong) * 25);
                }
            }

            // loang
            loang();

        }

        public void loang()
        {


            if (Hangdoi.Count != 0)
            {
                int _dong = Convert.ToInt32(Hangdoi.Pop().ToString());
                int _cot = Convert.ToInt32(Hangdoi.Pop().ToString());
                mootrong(_cot, _dong, grs, _cot * 25, _dong * 25);
            }
        }
        public void moochuaso(int _cot, int _dong, int x, int y)
        {

            if (co[_cot, _dong] == -2)
            {

                switch (mainboard[_cot, _dong])
                {

                    case 1: grs.DrawImage(Img_poit1, x, y); break;
                    case 2: grs.DrawImage(Img_poit2, x, y); break;
                    case 3: grs.DrawImage(Img_poit3, x, y); break;
                    case 4: grs.DrawImage(Img_poit4, x, y); break;
                    case 5: grs.DrawImage(Img_poit5, x, y); break;
                    case 6: grs.DrawImage(Img_poit6, x, y); break;
                    case 7: grs.DrawImage(Img_poit7, x, y); break;
                    case 8: grs.DrawImage(Img_poit8, x, y); break;

                }
                kiemtraoco[_cot, _dong] = 1;
            }
        }
        private void panel1_Paint(object sender, PaintEventArgs e)
        {
            vetoado();
        }
        #endregion
        public void hienbom()
        {

            for (int i = 0; i < Convert.ToInt32(nudrow.Text.ToString()); i++)
                for (int i2 = 0; i2 < Convert.ToInt32(nudcol.Text.ToString()); i2++)
                {
                    if (mainboard[i, i2] == -1)
                    {
                        grs.DrawImage(Img_bom, i * 25, i2 * 25);
                        if (co[i, i2] == -1)
                            grs.DrawImage(Img_bom_X, i * 25, i2 * 25);
                    }
                }
        }

        public void xulysukien(int _cot, int _dong, Graphics g, int x, int y)
        {
            if (co[_cot, _dong] == -2)
            {
                kiemtraoco[_cot, _dong] = 1;
                switch (mainboard[_cot, _dong])
                {
                    case -1: grs.DrawImage(Img_bom, x, y);
                        hienbom();
                        timer1.Enabled = false;
                        MessageBox.Show("Game over");
                        lbHienthi.Text = "10";
                        restart();
                        break;
                    case 0: grs.DrawImage(Img_poit9, x, y);
                        mootrong(_cot, _dong, g, x, y); break;
                    case 1: grs.DrawImage(Img_poit1, x, y); break;
                    case 2: grs.DrawImage(Img_poit2, x, y); break;
                    case 3: grs.DrawImage(Img_poit3, x, y); break;
                    case 4: grs.DrawImage(Img_poit4, x, y); break;
                    case 5: grs.DrawImage(Img_poit5, x, y); break;
                    case 6: grs.DrawImage(Img_poit6, x, y); break;
                    case 7: grs.DrawImage(Img_poit7, x, y); break;
                    case 8: grs.DrawImage(Img_poit8, x, y); break;
                }
            }
            // hàm kiểm tra win hay ko
            int youwin = 0;
            for (int i = 0; i < Convert.ToInt32(nudrow.Text.ToString()); i++)
            {
                for (int j = 0; j < Convert.ToInt32(nudcol.Text.ToString()); j++)
                {
                    if (mainboard[i, j] == co[i, j])
                    {
                        youwin++;

                    }
                    if (youwin == Convert.ToInt32(nudbom.Text.ToString()) && lblco.Text == "0")
                    {
                        timer1.Enabled = false;
                        MessageBox.Show("Xin chúc mừng bạn đã chiến thắng ^^");
                        j = Convert.ToInt32(nudrow.Text.ToString());
                        i = Convert.ToInt32(nudcol.Text.ToString());
                    }

                }

            }
        }
        public void vetoado()
        {
            for (int i = 0; i < Convert.ToInt32(nudrow.Text.ToString()); i++)
                for (int i2 = 0; i2 < Convert.ToInt32(nudcol.Text.ToString()); i2++)
                {
                    grs.DrawImage(Img_cell, i * 25, i2 * 25);
                }
        }


        // click chuột phải thì cắm cờ
        public void xulysukienco(int _cot, int _dong, Graphics g, int x, int y)
        {
            if (kiemtraoco[_cot, _dong] == 0)
            {
                if (co[_cot, _dong] == -2)
                {

                    grs.DrawImage(Img_co, x, y);
                    co[_cot, _dong] = -1;
                    lblco.Text = (Convert.ToInt32(lblco.Text) - 1).ToString();
                }
                else
                {
                    grs.DrawImage(Img_cell, x, y);
                    co[_cot, _dong] = -2;
                    lblco.Text = (Convert.ToInt32(lblco.Text) + 1).ToString();
                }
                int youwin = 0;
                for (int i = 0; i < Convert.ToInt32(nudrow.Text.ToString()); i++)
                {
                    for (int j = 0; j < Convert.ToInt32(nudcol.Text.ToString()); j++)
                    {
                        if (mainboard[i, j] == co[i, j])
                        {
                            youwin++;

                        }
                        if (youwin == Convert.ToInt32(nudbom.Text.ToString()) &&lblco.Text=="0")
                        {
                            timer1.Enabled = false;
                            MessageBox.Show("Xin chúc mừng bạn đã chiến thắng ^^");
                            j = Convert.ToInt32(nudrow.Text.ToString());
                            i = Convert.ToInt32(nudcol.Text.ToString());
                        }

                    }

                }
            }
        }

        // Bắt sự kiện click
        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            if (indextime == 1)
            {
                timer1.Enabled = true;
                time = 10;
                indextime = 2;
            }
            if (e.Button == MouseButtons.Right)
            {
                int _cot = e.X / 25;
                int _dong = e.Y / 25;
                int x = _cot * 25;
                int y = _dong * 25;
                xulysukienco(_cot, _dong, grs, x, y);
            }
            else
            {
                int _cot = e.X / 25;
                int _dong = e.Y / 25;
                int x = _cot * 25;
                int y = _dong * 25;
                xulysukien(_cot, _dong, grs, x, y);
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            lbHienthi.Text = "10";
            restart();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        
        private void butstart_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            lbHienthi.Text = "10";
            restart();
          
        }
        public void restart()
        {
            indextime = 1;
            time = 10;
            lblco.Text = nudbom.Text.ToString();
            mainboard = new int[Convert.ToInt32(nudrow.Text.ToString()), Convert.ToInt32(nudcol.Text.ToString())];
            co = new int[Convert.ToInt32(nudrow.Text.ToString()), Convert.ToInt32(nudcol.Text.ToString())];
            kiemtraoco = new int[Convert.ToInt32(nudrow.Text.ToString()), Convert.ToInt32(nudcol.Text.ToString())];
            khoitaomangmoi(Convert.ToInt32(nudrow.Text.ToString()), Convert.ToInt32(nudcol.Text.ToString()), Convert.ToInt32(nudbom.Text.ToString()));
            khoitaomangco(Convert.ToInt32(nudrow.Text.ToString()), Convert.ToInt32(nudcol.Text.ToString()));
            this.panel1.Size = new System.Drawing.Size(Convert.ToInt32(nudrow.Text.ToString()) * 25 + 1, Convert.ToInt32(nudcol.Text.ToString()) * 25 + 1);
           
            vetoado();
            grs = panel1.CreateGraphics();
        }

        // Tạo đồng hồ đếm ngược


        public void timer1_Tick(object sender, EventArgs e)
        {
            lbHienthi.Text =  time.ToString() + ":"+ time2.ToString(); 
            
            if (time2 != 0)
                time2--;
            else
            { time--;
            time2 = 60;
            }
            
            if (time < 0)
            {
                lbHienthi.Text = "0000";
                timer1.Enabled = false;
                MessageBox.Show("Game over");
                restart();
            }
        }

        private void quitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void hướngDẫnChơiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"
- Người chơi khởi đầu với một bảng ô vuông trống thể hiện bãi mìn

- Click chuột vào một ô vuông trong bảng. Nếu không may trúng phải ô có mìn (điều này ít xảy ra hơn) thì người chơi trò chơi kết thúc. Trường hợp thường xảy ra hơn là ô đó không có mìn và một vùng các ô sẽ được mở ra cùng với những con số. Số trên một ô là chỉ số ô có mìn trong cả thảy 8 ô nằm lân cận với ô đó.

- Nếu chắc chắn một ô có mìn, người chơi đánh dấu vào ô đó bằng hình lá cờ (click chuột phải).

- Trò chơi kết thúc với phần thắng dành cho người chơi nếu tìm được tất cả các ô có mìn và mở được tất cả các ô không có mìn.","Hướng dẫn chơi");
        }

        private void thôngTinToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(@"đại học mở tphcm");
        }

       
    }
}