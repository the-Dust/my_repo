using System;
using System.Windows.Forms;

class MyButton : Form
{
    private Button mrButton;
    public MyButton()
    {
        mrButton = new Button();
        mrButton.Text = "Press Me";
        mrButton.Top = 100;
        mrButton.Left = 100;
        mrButton.Height = 50;
        mrButton.Width = 70;
        mrButton.Click += new EventHandler(MyButtonClickEventHandler);
        mrButton.MouseEnter += new EventHandler(mrButton_MouseEnter);
        this.MouseDown += new MouseEventHandler(TheMouseIsDown);
        this.Controls.Add(mrButton);
    }
    static void Main()
    {
        Application.Run(new MyButton()); 
    }
    void MyButtonClickEventHandler(object sender, EventArgs e)
    {
        mrButton.Text = "You pressed me";
    }
    public void TheMouseIsDown(object sender, MouseEventArgs e)
    {
        if (e.Button == MouseButtons.Left)
        {
            this.Text = "Left button pressed";
            this.Width = this.Width + 10;
        }
        else if (e.Button == MouseButtons.Right)
        {
            this.Text = "Right button pressed";
            this.Width = this.Width - 10;
        }
    }
    public void mrButton_MouseEnter(object sender, EventArgs e)
    {
        Random rnd1 = new Random();
        mrButton.Top = rnd1.Next(0, (this.Height - mrButton.Height));
        mrButton.Left = rnd1.Next(0, (this.Width - mrButton.Width));
    }

}