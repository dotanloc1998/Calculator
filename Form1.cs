using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Calculator
{
    public partial class MiniCalculator : Form
    {
        public MiniCalculator()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }
        public class Stack<T>
        {
            class Node
            {
                public T data;
                public Node next;
                public Node(T a, Node b)
                {
                    data = a;
                    next = b;
                }
            }
            private Node head;
            public int CountStack()
            {
                Node temp = head;
                int i = 0;
                for (; temp != null; temp = temp.next)
                {
                    i++;
                }
                return i;
            }
            public T Top()
            {
                return head.data;
            }
            public void Push(T a)
            {
                Node temp = new Node(a, head);
                head = temp;
            }
            public void Pop()
            {
                head = head.next;
            }
            public bool IsEmpty()
            {
                if (head == null)
                {
                    return true;
                }
                return false;
            }
        }
        public string MidtoPosfix()
        {
            Stack<string> a = new Stack<string>();
            string ketQuaHauTo = "";
            string tokken;
            string nhap = textBox1.Text;
            nhap = nhap.Trim();
            string s = "";
            for (int i = 0; i < nhap.Length; i++)
            {
                if (nhap[i] != ' ')
                {
                    s += nhap[i];
                }
            }
            int demMoNgoac = 0;
            int demDongNgoac = 0;
            int demToanTu = 0;
            int demToanHang = 0;
            for (int i = 0; i < s.Length;)
            {
                GetTokken(s, out tokken, ref i);
                if (tokken[0] == '(')
                {
                    demMoNgoac++;
                }
                else if (tokken[0] == ')')
                {
                    demDongNgoac++;
                }
                else if (LaToanTu(tokken[0]))
                {
                    demToanTu++;
                }
                else
                {
                    demToanHang++;
                }
            }
            if (demDongNgoac == demMoNgoac && demToanHang - 1 == demToanTu)
            {
                for (int i = 0; i < s.Length;)
                {
                    GetTokken(s, out tokken, ref i);
                    if (!LaToanTu(tokken[0]))
                    {
                        ketQuaHauTo += tokken + " ";
                    }
                    else
                    {
                        if (tokken[0] == '(')
                        {
                            a.Push(tokken);
                        }
                        else if (tokken[0] == ')')
                        {
                            while (a.Top()[0] != '(')
                            {
                                ketQuaHauTo += a.Top() + " ";
                                a.Pop();
                            }
                            if (a.Top()[0] == '(')
                            {
                                a.Pop();
                            }
                        }
                        else
                        {
                            if (!a.IsEmpty() && DoUuTien(a.Top()[0]) < DoUuTien(tokken[0]))
                            {
                                a.Push(tokken);
                                continue;
                            }
                            else if (a.IsEmpty())
                            {
                                a.Push(tokken);
                                continue;
                            }
                            while (!a.IsEmpty() && DoUuTien(a.Top()[0]) >= DoUuTien(tokken[0]))
                            {
                                ketQuaHauTo += a.Top() + " ";
                                a.Pop();
                            }
                            a.Push(tokken);
                        }
                    }
                }
                while (!a.IsEmpty())
                {
                    if (a.Top()[0] == '(')
                    {
                        a.Pop();
                        continue;
                    }
                    ketQuaHauTo += a.Top() + " ";
                    a.Pop();

                }
                return ketQuaHauTo;
            }
            else
            {
                return "\nBieu thuc sai";
            }
        }

        public void SolvePosfix(string posfix)
        {
            string[] containValues = posfix.Split(' ');
            Stack<string> containResult = new Stack<string>();
            for (int i = 0; i < containValues.Length - 1; i++)
            {
                if (!LaToanTu(containValues[i][0]))
                {
                    containResult.Push(containValues[i]);
                }
                else
                {
                    string a = containResult.Top();
                    containResult.Pop();
                    string b = containResult.Top();
                    containResult.Pop();
                    containResult.Push(Calculate(containValues[i], b, a));
                }
            }
            double kq = Convert.ToDouble(containResult.Top());
            textBox1.Text = Convert.ToString(kq);
        }

        public string Calculate(string toanTu, string toanHang, string toanHang2)
        {
            double a = Convert.ToDouble(toanHang);
            double b = Convert.ToDouble(toanHang2);
            if (toanTu[0] == '+')
            {
                return Convert.ToString(a + b);
            }
            else if (toanTu[0] == '-')
            {
                return Convert.ToString(a - b);
            }
            else if (toanTu[0] == '*')
            {
                return Convert.ToString(a * b);
            }
            else if (toanTu[0] == '/' && b != 0)
            {
                return Convert.ToString(a / b);
            }
            return "?";
        }
        public void GetTokken(string chuoiNhap, out string tokken, ref int start)
        {
            tokken = "";
            for (; start < chuoiNhap.Length; start++)
            {
                if (LaToanTu(chuoiNhap[start]))
                {
                    if (tokken == "")
                    {
                        tokken += chuoiNhap[start];
                        start++;
                        return;
                    }
                    else
                    {
                        return;
                    }
                }
                else
                {
                    tokken += chuoiNhap[start];
                }
            }
        }
        public int DoUuTien(char a)
        {
            if (a == '+' || a == '-')
            {
                return 1;
            }
            else if (a == '*' || a == '/')
            {
                return 2;
            }
            return 0;
        }
        public bool LaToanTu(char a)
        {
            if (a == '+' || a == '-' || a == '*' || a == '/' || a == '(' || a == ')')
            {
                return true;
            }
            return false;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button15_Click(object sender, EventArgs e)
        {
            textBox1.Text += '/';
            label1.Focus();
        }

        private void button14_Click(object sender, EventArgs e)
        {
            textBox1.Text = "";
            label1.Focus();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            textBox1.Text += '8';
            label1.Focus();
        }

        private void nut2_Click(object sender, EventArgs e)
        {
            textBox1.Text += '2';
            label1.Focus();
        }

        private void nut1_Click(object sender, EventArgs e)
        {
            textBox1.Text += '1';
            label1.Focus();
        }

        private void nut3_Click(object sender, EventArgs e)
        {
            textBox1.Text += '3';
            label1.Focus();
        }

        private void nut4_Click(object sender, EventArgs e)
        {
            textBox1.Text += '4';
            label1.Focus();
        }

        private void nut5_Click(object sender, EventArgs e)
        {
            textBox1.Text += '5';
            label1.Focus();
        }

        private void nut6_Click(object sender, EventArgs e)
        {
            textBox1.Text += '6';
            label1.Focus();
        }

        private void nut7_Click(object sender, EventArgs e)
        {
            textBox1.Text += '7';
            label1.Focus();
        }

        private void nut9_Click(object sender, EventArgs e)
        {
            textBox1.Text += '9';
            label1.Focus();
        }

        private void nutDelete_Click(object sender, EventArgs e)
        {

            string a = textBox1.Text;
            textBox1.Text = "";
            for (int i = 0; i < a.Length - 1; i++)
            {
                textBox1.Text += a[i];
            }
            label1.Focus();
        }

        private void nutNhan_Click(object sender, EventArgs e)
        {
            textBox1.Text += '*';
            label1.Focus();
        }

        private void nutTru_Click(object sender, EventArgs e)
        {
            textBox1.Text += '-';
            label1.Focus();
        }

        private void nutCong_Click(object sender, EventArgs e)
        {
            textBox1.Text += '+';
            label1.Focus();
        }

        private void dauBang_Click(object sender, EventArgs e)
        {
            string posfix = MidtoPosfix();
            try
            {
                SolvePosfix(posfix);
            }
            catch (Exception exception)
            {
                textBox1.Text = "Syntax Error";
            }
        }

        private void dauCham_Click(object sender, EventArgs e)
        {
            textBox1.Text += '.';
            label1.Focus();
        }

        private void nut0_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                textBox1.Text += "0";
            }
            label1.Focus();
        }

        private void moNgoac_Click(object sender, EventArgs e)
        {
            textBox1.Text += '(';
            label1.Focus();
        }

        private void dongNgoac_Click(object sender, EventArgs e)
        {
            textBox1.Text += ')';
            label1.Focus();
        }
    }
}
