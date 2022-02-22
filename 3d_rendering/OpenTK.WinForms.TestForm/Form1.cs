using System;
using System.Windows.Forms;
using LearnOpenTK.Common;
using OpenTK.Graphics.OpenGL;
using OpenTK.Mathematics;

namespace OpenTK.WinForms.TestForm
{
    public partial class Form1 : Form
	{
        private bool _firstMove = true;
        private Vector2 _lastPos;

        private Timer _timer = null!;
        private float _angle = 0.0f;

        private System.Drawing.Point mousePos = new System.Drawing.Point(0, 0);
        private System.Drawing.Point mousePosOriginal = new System.Drawing.Point(0, 0);
        private bool mouseClicked = false;

        private Camera _camera;
        const float cameraSpeed = 1.5f;
        const float sensitivity = 0.2f;

        public float _screen_x = 0.0f;
        public float _screen_y = 0.0f;

        private float _x = 0.0f;
        private float _y = 0.0f;
        private float _z = 0.0f;

        private float _zx = 0.0f;
        private float _zy = 5.0f;
        private float _zz = 5.0f;

        public double lin;


        public Form1()
		{
			InitializeComponent();
            _lastPos = new Vector2(mousePos.X, mousePos.Y);
            _firstMove = false;
            this.KeyPreview = true;
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show(this,
                "This demonstrates a simple use of the new OpenTK 4.x GLControl.",
                "GLControl Test Form",
                MessageBoxButtons.OK);
        }

        private void glControl_Load(object? sender, EventArgs e)
        {
            // Make sure that when the GLControl is resized or needs to be painted,
            // we update our projection matrix or re-render its contents, respectively.
            glControl.Resize += glControl_Resize;
            glControl.Paint += glControl_Paint;

            // Redraw the screen every 1/20 of a second.
            _timer = new Timer();
            _timer.Tick += (sender, e) =>
            {
                //물체를 실시간을 돌리기 위해서는 주석을 해제하면 된다.
                //_angle += 0.5f; 
                Render();
            };
            _timer.Interval = 50;   // 1000 ms per sec / 50 ms per frame = 20 FPS
            _timer.Start();

            // Ensure that the viewport and projection matrix are set correctly initially.
            glControl_Resize(glControl, EventArgs.Empty);
        }

        private void glControl_Resize(object? sender, EventArgs e)
        {
            glControl.MakeCurrent();

            if (glControl.ClientSize.Height == 0)
                glControl.ClientSize = new System.Drawing.Size(glControl.ClientSize.Width, 1);

            // GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);
            GL.Viewport(0, 0, glControl.ClientSize.Width, glControl.ClientSize.Height);

            float aspect_ratio = Math.Max(glControl.ClientSize.Width, 1) / (float)Math.Max(glControl.ClientSize.Height, 1);
            Matrix4 perpective = Matrix4.CreatePerspectiveFieldOfView(MathHelper.PiOver4, aspect_ratio, 1, 64);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref perpective);
        }

        private void glControl_Paint(object sender, PaintEventArgs e)
        {
            Render();
        }

        private void Render()
        {
            var test_xyz_arr = new float[,] { };
            var test_rgb_arr = new int[,] { };

            glControl.MakeCurrent();

            GL.ClearColor(Color4.MidnightBlue);
            GL.Enable(EnableCap.DepthTest);
            GL.PointSize(5.0f);

            Matrix4 lookat = Matrix4.LookAt(_zx, _zy, _zz, _x, _y, _z, 0, 1, 3);
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref lookat);

            GL.Rotate(_angle, 5.0f, 1.0f, 0.0f);

            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            GL.Begin(BeginMode.Points);

            GL.Color4(Color4.Silver);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);

            GL.Color4(Color4.Red);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);


            GL.Color4(Color4.Green);
            GL.Vertex3(-1.0f, -1.0f, -1.0f);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);

            GL.Color4(Color4.Black);
            GL.Vertex3(-1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);

            GL.Color4(Color4.Yellow);
            GL.Vertex3(-1.0f, 1.0f, -1.0f);
            GL.Vertex3(-1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);

            GL.Color4(Color4.LimeGreen);
            GL.Vertex3(1.0f, -1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, -1.0f);
            GL.Vertex3(1.0f, 1.0f, 1.0f);
            GL.Vertex3(1.0f, -1.0f, 1.0f);

            //test_xyz_arr = objFile_Render.objFile_Render.ReadXyz();
            //test_rgb_arr = objFile_Render.objFile_Render.ReadRgb();
            //objFile_Render.objFile_Render.xyzrgb_Render(test_xyz_arr, test_rgb_arr);

            GL.End();

            //test용
            var p1 = new double[] {3.0, 5.0, -1.0};
            var p2 = new double[] {2.0, 1.0, 7.0 };
            var p3 = new double[] {5.0, 7.0, 8.0 };
            //TEST : 6.4454

            lin = Point.Point.PointToLine(p1, p2, p3);

            glControl.SwapBuffers();
        }

        
        private void glControl_MouseClick(object sender, MouseEventArgs e)
        {
            this.KeyPreview = true;
            if(e.Button == MouseButtons.Left)
            {
                X_textBox.Clear();
                Y_textBox.Clear();
                Z_textBox.Clear();
                _screen_x = e.X;
                _screen_y = e.Y;

                X_textBox.Text = _screen_x.ToString();
                Y_textBox.Text = _screen_y.ToString();
            }
            
        }

        private void glControl_keyevent(object sender, KeyEventArgs e)
        {
            this.KeyPreview = true;
            glControl.Resize += glControl_Resize;
            glControl.Paint += glControl_Paint;
            Z_textBox.Clear();
            Z_textBox.Text = lin.ToString();

            if (e.KeyCode == Keys.A)
            {
                _x -= 0.1f;
                //X_textBox.Clear();
               // X_textBox.Text = "F";
                //Y_textBox.Clear();
               // Y_textBox.Text = e.KeyCode.ToString();
            }
            else if(e.KeyCode == Keys.D)
            {
                _x += 0.1f;
            }
            else if (e.KeyCode == Keys.W)
            {
                _y += 0.1f;
            }
            else if (e.KeyCode == Keys.S)
            {
                _y -= 0.1f;
            }
            else if (e.KeyCode == Keys.Space)
            {
                _z += 0.1f;
            }
            else if (e.KeyCode == Keys.Q)
            {
                _z -= 0.1f;
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.P)
            {
                _angle -= 0.5f;
            }
            else if (e.KeyCode == System.Windows.Forms.Keys.L)
            {
                _angle += 0.5f;
            }

            Render();
            glControl_Resize(glControl, EventArgs.Empty);
        }

        private void glControl_MouseWheel(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            glControl.Resize += glControl_Resize;
            glControl.Paint += glControl_Paint;
            if ((e.Delta / 120) > 0)
            {
                // _zx += 0.5f;
                _zy += 0.5f;
                _zz += 0.5f;
                // _angle += 0.5f;
            }
            else
            {
                // _zx -= 0.5f;
                _zy -= 0.5f;
                _zz -= 0.5f;
                //_angle -= 0.5f;
            }
            Render();
            glControl_Resize(glControl, EventArgs.Empty);
        }
        /*
        private void glControl_MouseMove(object sender, MouseEventArgs e)
        {
            
            this.mousePos.X = e.X;
            this.mousePos.Y = e.Y;
            if (e.Button == MouseButtons.Left && this.mouseClicked)
            {
                textBox1.Clear();
                textBox1.Text = "마우스 이동";
                // Calculate the offset of the mouse position
                var deltaX = mousePos.X - _lastPos.X;
                var deltaY = mousePos.Y - _lastPos.Y;
                _lastPos = new Vector2(mousePos.X, mousePos.Y);

                // Apply the camera pitch and yaw (we clamp the pitch in the camera class)
                _camera.Yaw += deltaX * sensitivity;
                _camera.Pitch -= deltaY * sensitivity; // Reversed since y-coordinates range from bottom to top
            }
        }
        private void glControl_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                this.mouseClicked = false;
                this.GLrender.ConsolidateMove();
            }
            if (e.Button != MouseButtons.Right || this.GLrender.Models3D.Count <= 0)
                return;
            this.clickedDirect = false;
            this.GLrender.ConsolidaMoveModel();
        }

        private void glControl_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left && !this.mouseClicked)
            {
                this.mouseClicked = true;
                mousePosOriginal.X = e.X;
                mousePosOriginal.Y = e.Y;
            }
            if (e.Button != MouseButtons.Right || this.clickedDirect)
                return;
            this.clickedDirect = true;
            this.xOrigin = e.X;
            this.yOrigin = e.Y;
        }
        */


    }
}
