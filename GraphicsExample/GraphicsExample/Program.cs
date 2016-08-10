using System;
using System.Windows.Forms;
using System.Drawing;

class MyButton : Form
{
    private Button mrButton;

    public MyButton()
    {
    mrButton = new Button();
    mrButton.Text = "Button";
    mrButton.Click += new System.EventHandler(MyButtonClick);
    this.Controls.Add(mrButton);
    this.MouseMove += new System.Windows.Forms.MouseEventHandler(TheMouseMoved);
    }
    
    static void Main()
    {
    Application.Run(new MyButton());
    }
    void MyButtonClick(object sender, EventArgs e)
    {
    mrButton.Text = "Yahoo";
    }

    public void TheMouseMoved(object sender, MouseEventArgs e)
    {
        System.Drawing.Graphics g = this.CreateGraphics();
        System.Drawing.Pen redPen = new System.Drawing.Pen(Color.Green, 1);
        g.DrawEllipse(redPen, e.X, e.Y, 40, 100);
        g.Dispose();
    }

}
