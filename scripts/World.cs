using System.Collections.Generic;
using Godot;
using System;

public class World : Spatial
{
    
    private Control pause_menu;
    private Player player;
    private Spatial finNiv;
    private Area finNivArea;
    private Control loading;
    private global_arcade global_Arcade;

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
        SpatialMaterial finMat = new SpatialMaterial();
        finMat.AlbedoColor = new Godot.Color(0.0F,0.5F,0F);

        List<MeshInstance> liste = new List<MeshInstance> {};
        
        //Create the blocks
        int nbblocks=10;
        int max_security=50;
        int security=0;
        for(int w=0; w<=nbblocks; w++){
            if(security==max_security){
                break;
            }
            //The Mesh : 
            MeshInstance mesh = new MeshInstance();
            mesh.Name="platform_"+Convert.ToString(w);
            mesh.Mesh = new CubeMesh();
            mesh.Scale = new Vector3(tx,ty,tz);
            mesh.Translation = new Vector3(xx,yy,zz);
            //Add the collision
            mesh.CreateTrimeshCollision();
            //Add the collision security shape
            Area ca = new Area();
            ca.Name="security_zone";
            CollisionShape cs = new CollisionShape();
            cs.Shape=new BoxShape();
            cs.Scale=new Vector3(1,3,1);
            ca.AddChild(cs);
            mesh.AddChild(ca);
            //Add the material
            mesh.MaterialOverride = groundMat;
            //add it to the scene
            AddChild(mesh);
            liste.Add(mesh);
            //Change the coordinates for the next one
            if(w!=nbblocks){
                bool derange=false;
                security=0;
                int dx=0;
                int dy=0;
                int dz=0;
                do{
                    var rand = new Random();
                    dx=rand.Next(2,5)*tx;
                    dz=rand.Next(2,5)*ty;
                    dy=0;
                    if(xx!=0 && zz!=0){
                        dy=rand.Next(5,6);
                    }
                    //
                    if(rand.Next(1,3)==2){ dx*=-1; }
                    if(rand.Next(1,3)==2){ dy*=-1; }
                    if(rand.Next(1,3)==2){ dz*=-1; }
                
                    //Create The Future Mesh to test
                    MeshInstance next_mesh = new MeshInstance();
                    next_mesh.Mesh = new CubeMesh();
                    next_mesh.Scale = new Vector3(tx,ty,tz);
                    next_mesh.Translation = new Vector3(xx+dx,yy+dy,zz+dz);
                    //Add the collision
                    next_mesh.CreateTrimeshCollision();
                    AddChild(next_mesh);

                    for(int i=0; i<w; i++){
                        Area cia=(Area)GetNode("platform_"+Convert.ToString(w)+"/security_zone");
                        //GD.Print(cia.OverlapsArea(next_mesh));
                        if(cia.OverlapsArea(next_mesh)){
                            derange=true;
                            GD.Print("deranged");
                        }
                    }

                    RemoveChild(next_mesh);

                }while(derange && security<max_security);
                
                if(!(security==max_security)){
                    xx+=dx;
                    yy+=dy;
                    zz+=dz;
                    //
                    if(zz<player.hauteur_min+ty){
                        zz=Convert.ToInt32(player.hauteur_min)+ty;
                    }
                }
                
            }
        }
        //on donne les coordonnées à la fin du niveau
        finNiv.Translation = new Vector3(xx , yy+ty+0.3F, zz);

        
    }

    public void mapCreation(){
        string method="platforms";
        if(method == "platforms"){ GeneratePlatformMethod(); }
    }

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {   
        //
        global_Arcade = (global_arcade)GetNode("/root/GlobalArcade");
        //
        player= (Player)GetNode("Player");
        player.Connect("onPauseBtPress", this, nameof(Pause));
        //
        finNiv = (Spatial)GetNode("FinNiv");
        finNivArea = (Area)GetNode("FinNiv/Area");
        //
        mapCreation();
        //
        loading= (Control)GetNode("Loading");
        loading.Visible=false;
        //
        pause_menu = (Control)GetNode("Pause_Menu");
        pause_menu.Visible=false;
        pause_menu.Connect("resume", this, nameof(onPauseMenuBtResumePress));
        //Input.SetMouseMode(Input.MouseMode.Captured);
        
    }

    public void nivFini(){
        GetTree().ChangeScene("res://levels/World.tscn");
    }

    public void onPauseMenuBtResumePress(){
        Resume();
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

   

    // Called every frame. 'delta' is the elapsed time since the previous frame.
    public override void _Process(float delta)
    {
        if(finNivArea.OverlapsBody(player)){
            nivFini();
        }

    }
}

