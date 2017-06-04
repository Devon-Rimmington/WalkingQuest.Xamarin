using System;
using System.Diagnostics;
using Urho;
using Urho.Gui;
using Urho.Actions;
using WalkingQuest.GUI;

namespace WalkingQuest
{
    public class MyGame : Application
    {

        // GUI elements
        private Window window;
        private Button signInButton;
        private TextEdit username, password;


        [Preserve]
        public MyGame(ApplicationOptions opts) : base(opts) { }

        static MyGame()
        {
            /*
            UnhandledException += (s, e) =>
            {
                if (Debugger.IsAttached)
                    Debugger.Break();
                e.Handled = true;
            };
            */
        }

        protected override void Start()
        {
            CreateScene();

            // Subscribe to Esc key:
            Input.SubscribeToKeyDown(args => { if (args.Key == Key.Esc) Exit(); });
        }

        async void CreateScene()
        {
            /*
            // UI text 
            helloText = new Text(Context);
            helloText.Value = "Hello World from UrhoSharp";
            helloText.HorizontalAlignment = HorizontalAlignment.Center;
            helloText.VerticalAlignment = VerticalAlignment.Top;
            helloText.SetColor(new Color(r: 0f, g: 1f, b: 1f));
            helloText.SetFont(font: ResourceCache.GetFont("Fonts/Font.ttf"), size: 30);
            UI.Root.AddChild(helloText);
            */


            // create the UI object for the sign-in process
            InitGUI();

            // 3D scene with Octree
            var scene = new Scene(Context);
            scene.CreateComponent<Octree>();

            // Box	
            Node boxNode = scene.CreateChild(name: "Box node");
            boxNode.Position = new Vector3(x: 0, y: 0, z: 5);
            boxNode.SetScale(0f);
            boxNode.Rotation = new Quaternion(x: 60, y: 0, z: 30);

            StaticModel boxModel = boxNode.CreateComponent<StaticModel>();
            boxModel.Model = ResourceCache.GetModel("Models/Box.mdl");
            boxModel.SetMaterial(ResourceCache.GetMaterial("Materials/BoxMaterial.xml"));

            // Light
            Node lightNode = scene.CreateChild(name: "light");
            var light = lightNode.CreateComponent<Light>();
            light.Range = 10;
            light.Brightness = 1.5f;

            // Camera
            Node cameraNode = scene.CreateChild(name: "camera");
            Camera camera = cameraNode.CreateComponent<Camera>();

            // Viewport
            Renderer.SetViewport(0, new Viewport(Context, scene, camera, null));

            // Do actions
            await boxNode.RunActionsAsync(new EaseBounceOut(new ScaleTo(duration: 1f, scale: 1)));
            await boxNode.RunActionsAsync(new RepeatForever(
                new RotateBy(duration: 1, deltaAngleX: 90, deltaAngleY: 0, deltaAngleZ: 0)));
        }

        public void InitGUI()
        {
            window = new Window(Context);
            window.SetMinSize(384, 192);
            window.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Top);
            window.SetLayout(LayoutMode.Vertical, 100, new IntRect(6, 6, 6, 6));
            UI.Root.AddChild(window);

            // first box is the username/password input
            
            username = new TextEdit(Context, this);
            username.Text = "username...";
			// username.MaxWidth = 100;
			username.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Top);
            username.MinHeight = 100;
            username.SetColor(new Color(1f, 1f, 0f));
            window.AddChild(username);
                        

            // todo: remove this
            
            Action<Urho.Gui.TextChangedEventArgs> textChangedAction = (e) => SignedIn();
            username.TextChanged += (textChangedAction);


            password = new TextEdit(Context, this);
            password.Text = "password...";
            password.MinHeight = 100;
            password.SetColor(new Color(1f, 0f, 1f));
            window.AddChild(password);


            // reigister the action that the button will perform after the click
            Action<Urho.Gui.PressedEventArgs> signInAction = (e) => SignedIn();

            signInButton = new Button(Context);
            signInButton.Pressed += (signInAction);

            // signInButton.SetPosition(10, 10);
            signInButton.SetFixedSize(300, 135);
            signInButton.SetColor(new Color(0f, 1f, 1f));

            Text buttonText = new Text();
            buttonText.Value = "Signin";
            buttonText.SetFont(ResourceCache.GetFont("Fonts/Font.ttf"), size: 30);
            buttonText.SetAlignment(HorizontalAlignment.Center, VerticalAlignment.Center);
            signInButton.AddChild(buttonText);

            username.SetStyleAuto(null);
            password.SetStyleAuto(null);
            signInButton.SetStyleAuto(null);

            window.AddChild(signInButton);
        }

        private void Username_TextChanged(TextChangedEventArgs obj)
        {
            throw new NotImplementedException();
        }

        public bool SignedIn()
        {
            Debug.WriteLine(username.Text.ToString());
            return false;
        }

        public void UpdateStepCount(Int64 count)
        {
            // helloText.Value = count + "";
        }

    }
}
