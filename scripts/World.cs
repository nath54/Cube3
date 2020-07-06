using Godot;
using System;

public class World : Spatial
{
    
    private Control pause_menu;
    private Player player;

    private Control loading;

    public void GeneratePlatformMethod(){
        //Base coordinates
        int xx = 0,
            yy = 0,
            zz = 0;
        int tx = 3,
            ty = 2,
            tz = 3;
        //Create the Base Materials
        SpatialMaterial groundMat = new SpatialMaterial();
        groundMat.AlbedoColor = new Godot.Color(0.8F,0F,0F);
        groundMat.Metallic=1;       

        
        //Create the blocks
        for(int w=0; w<10; w++){
            //The Mesh : 
            MeshInstance mesh = new MeshInstance();
            mesh.Mesh = new CubeMesh();
            mesh.Scale = new Vector3(tx,ty,tz);
            mesh.Translation = new Vector3(xx,yy,zz);
            //Add the collision
            mesh.CreateTrimeshCollision();
            //Add the material
            mesh.MaterialOverride = groundMat;
            //add it to the scene
            AddChild(mesh);
            //Change the coordinates for the next one
            var rand = new Random();
            xx+=rand.Next(-5,5)*tx;
            zz+=rand.Next(-5,5)*ty;
            if(xx!=0 && zz!=0){
                yy+=rand.Next(-6,6);
            }
            if(zz<player.hauteur_min+ty){
                zz=Convert.ToInt32(player.hauteur_min)+ty;
            }
        }
    }

    public void mapCreation(){
        string method="platforms";
        if(method == "platforms"){ GeneratePlatformMethod(); }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        //
        player= (Player)GetNode("Player");
        player.Connect("pause_bt_press", this, nameof(onPauseBtPress));
        //
        mapCreation();
        //
        loading= (Control)GetNode("Loading");
        loading.Visible=false;
        //
        pause_menu = (Control)GetNode("Pause_Menu");
        pause_menu.Visible=false;
        pause_menu.Connect("resume", this, nameof(onPauseMenuBtResumePress));
        Input.SetMouseMode(Input.MouseMode.Captured);
        
    }

    public void onPauseMenuBtResumePress(){
        //GD.Print("resume !");
        Resume();
    }

    public void onPauseBtPress(){
        Pause();
    }

    public void Resume(){
        pause_menu.Visible=false;
        Input.SetMouseMode(Input.MouseMode.Captured);
        player.paused=false;
    }

    public void Pause(){
        pause_menu.Visible=true;
        Input.SetMouseMode(Input.MouseMode.Visible);
        player.paused=true;
    }

    public override void _Input(InputEvent @event){
        if(Input.IsActionJustPressed("menu")){
            if(pause_menu.Visible){
                Resume();
            }
            else{
                Pause();
            }
        }
    }

   

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}

