using System.Collections.Generic;
using Godot;
using System;

public class Settings_graphics : Control
{
    private Button bt_s_game;
    private Button bt_s_graphics;
    private Button bt_s_audio;
    private Button bt_s_other;
    private TextEdit te_width;
    private TextEdit te_height;
    private CheckBox cb_showfps;

    public struct AncientSettings{
        public static int width=(int)ProjectSettings.GetSetting("display/window/size/width");
        public static int height=(int)ProjectSettings.GetSetting("display/window/size/height");        
    }
    
    public VScrollBar vscrollbare;
    public VBoxContainer vboxcontainere;
    public Global globale;
    public PopupDialog popup;

    public int[] anisotropics = {2, 4 ,8 ,16};
    public int[] msaas = {2, 4 ,8 ,16};
    public int[] atlas_sizes = {0, 128, 512, 1024, 2048, 4096, 8192};
    public int[] shadow_resolutions = {0, 128, 512, 1024, 2048, 4096, 8192};
    public string[] sscs = {"low","medium","high"};
    public string[] saos = {"off","low","medium","high"};

    public bool is_mobile(){
		return (OS.GetName()=="Android" || OS.GetName()=="iOS");
	}

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        globale=(Global)GetNode("/root/Global");
        //
        bt_s_game = (Button)GetNode("Menu_Buttons/HScrollBar/Bt_game");
        bt_s_graphics = (Button)GetNode("Menu_Buttons/HScrollBar/Bt_graphics");
        bt_s_audio = (Button)GetNode("Menu_Buttons/HScrollBar/Bt_audio");
        bt_s_other = (Button)GetNode("Menu_Buttons/HScrollBar/Bt_other");
        cb_showfps = (CheckBox)GetNode("Settings/VBoxContainer/St_showfps/Cb_showfps");
        te_width = (TextEdit)GetNode("Settings/VBoxContainer/St_dwidth/te_width");
        te_height = (TextEdit)GetNode("Settings/VBoxContainer/St_dheight/te_height");
        te_width.Text=""+ProjectSettings.GetSetting("display/window/size/width");
        cb_showfps.Pressed=globale.aff_fps;
        te_height.Text=""+ProjectSettings.GetSetting("display/window/size/height");
        //
        popup = (PopupDialog)GetNode("PopupDialog");
        vscrollbare = (VScrollBar)GetNode("VScrollBar");
        vboxcontainere = (VBoxContainer)GetNode("Settings/VBoxContainer");
        //
        
    }

    public void _on_Bt_game_pressed(){ GetTree().ChangeScene("res://menus/Settings_game.tscn"); }
    public void _on_Bt_graphics_pressed(){ GetTree().ChangeScene("res://menus/Settings_graphics.tscn"); }
    public void _on_Bt_audio_pressed(){ GetTree().ChangeScene("res://menus/Settings_audio.tscn"); }
    public void _on_Bt_other_pressed(){ GetTree().ChangeScene("res://menus/Settings_other.tscn"); }

    public void _on_te_height_text_changed(){
        string val=te_height.Text;
        if(val=="default"){
            te_width.Text="1280";
            te_height.Text="720";
        }
        else{
            if(val.Length>=1){
                double value;
                if (double.TryParse(val, out value)){
                    int nvalue=Convert.ToInt32(value*(16.0/9.0));
                    string nval=""+nvalue;
                    te_width.Text=nval;
                }
                else{
                    te_height.Text=(string)ProjectSettings.GetSetting("display/window/size/height");
                }
            }
        }
    }

    public void _on_te_width_text_changed(){
        string val=te_width.Text;
        if(val=="default"){
            te_width.Text="1280";
            te_height.Text="720";
        }
        else{
            if(val.Length>=1){
                double value;
                if (double.TryParse(val, out value)){
                    double nvalue=Convert.ToInt32(value/(16.0/9.0));
                    string nval=""+nvalue;
                    te_height.Text=nval;
                }
                else{
                    te_width.Text=(string)ProjectSettings.GetSetting("display/window/size/width");
                }
            }
        }
    }

    public void on_apply(){
        //
        AncientSettings.width=(int)ProjectSettings.GetSetting("display/window/size/width");
        AncientSettings.height=(int)ProjectSettings.GetSetting("display/window/size/height");
        //
        int width=Convert.ToInt32(te_width.Text);
        int height=Convert.ToInt32(te_height.Text);
        //FPS
        globale.aff_fps=cb_showfps.Pressed;
        //Resolution
        ProjectSettings.SetSetting("display/window/size/width", (int)width);
        ProjectSettings.SetSetting("display/window/size/height", (int)height);
        //Vsync
        CheckBox cb_vsync = (CheckBox)GetNode("Settings/VBoxContainer/St_vsync/Cb");
        ProjectSettings.SetSetting("display/window/vsync/use_vsync",cb_vsync.Pressed);
        //3d effects
        CheckBox cb_effects = (CheckBox)GetNode("Settings/VBoxContainer/St_3d_effects/Cb");
        int value = 3;
        if(cb_effects.Pressed){ value = 2;}
        if( is_mobile() ){
            ProjectSettings.SetSetting("rendering/quality/intended_usage/framebuffer_allocation.mobile",value);
        }
        else{
            ProjectSettings.SetSetting("rendering/quality/intended_usage/framebuffer_allocation",value);
        }        
        //hdr
        CheckBox cb_hdr = (CheckBox)GetNode("Settings/VBoxContainer/St_vsync/Cb");
        if( is_mobile() ){
            ProjectSettings.SetSetting("rendering/quality/depth/hdr.mobile",cb_hdr.Pressed);
        }
        else{
            ProjectSettings.SetSetting("rendering/quality/depth/hdr",cb_hdr.Pressed);
        }   
        //Anisotropic
        HScrollBar sb_ani = (HScrollBar)GetNode("Settings/VBoxContainer/St_anisotropic/HScrollBar");
        int value_ani = anisotropics[(int)sb_ani.Value];
        ProjectSettings.SetSetting("rendering/quality/filters/anisotropic_filter_level", value_ani);
        //high quality ggx
        CheckBox cb_ggx = (CheckBox)GetNode("Settings/VBoxContainer/St_high_quality_ggx/Cb");
        if( is_mobile() ){
            ProjectSettings.SetSetting("rendering/quality/reflections/high_quality_ggx.mobile",cb_ggx.Pressed);
        }
        else{
            ProjectSettings.SetSetting("rendering/quality/reflections/high_quality_ggx",cb_ggx.Pressed);
        }  
        //Reflexion Atlas Size
        HScrollBar sb_ras = (HScrollBar)GetNode("Settings/VBoxContainer/St_reflex_atlas_size/HScrollBar");
        int value_ras = atlas_sizes[(int)sb_ras.Value];
        ProjectSettings.SetSetting("rendering/quality/reflections/atlas_size", value_ras);
        //Texture array reflexion
        CheckBox cb_tar = (CheckBox)GetNode("Settings/VBoxContainer/St_high_quality_ggx/Cb");
        if( is_mobile() ){
            ProjectSettings.SetSetting("rendering/quality/reflections/texture_array_reflections.mobile",cb_tar.Pressed);
        }
        else{
            ProjectSettings.SetSetting("rendering/quality/reflections/texture_array_reflections",cb_tar.Pressed);
        }
        //Force vertex shading
        CheckBox cb_fvs = (CheckBox)GetNode("Settings/VBoxContainer/St_force_vertex_shading/Cb");
        if( is_mobile() ){
            ProjectSettings.SetSetting("rendering/quality/shading/force_vertex_shading.mobile",cb_fvs.Pressed);
        }
        else{
            ProjectSettings.SetSetting("rendering/quality/shading/force_vertex_shading",cb_fvs.Pressed);
        }
        //Shadow Atlas size
        HScrollBar sb_sas = (HScrollBar)GetNode("Settings/VBoxContainer/St_reflex_atlas_size/HScrollBar");
        int value_sas = atlas_sizes[(int)sb_sas.Value];
        if(is_mobile()){
            ProjectSettings.SetSetting("rendering/quality/shadow_atlas/size.mobile", value_sas);
        }
        else{
            ProjectSettings.SetSetting("rendering/quality/shadow_atlas/size", value_sas);
        }
        //Subsurface Scattering
        HScrollBar hsb_ssc = (HScrollBar)GetNode("Settings/VBoxContainer/St_subsurface_scattering/HScrollBar");
        ProjectSettings.SetSetting("rendering/quality/subsurface_scattering/quality",(int)hsb_ssc.Value);
        //Ambient Occlusion
        HScrollBar hsb_sao = (HScrollBar)GetNode("Settings/VBoxContainer/St_ambient_occlusion/HScrollBar");
        double value_sao = hsb_sao.Value;
        if(value_sao==0){
            globale.sao=false;
        }
        else{
            globale.sao=true;
            globale.sao_quality=(int)value_sao-1;
        }        

        //Save Settings
        ProjectSettings.Save();
        globale.SaveGame();
        //
        if(width!=AncientSettings.width || height!=AncientSettings.height){
            popup.Popup_();

        }
    }

    public void on_anis_change(float value){
        Label txt = (Label)GetNode("Settings/VBoxContainer/St_anisotropic/Txt");
        txt.Text = "Anisotropic Filter Level : "+anisotropics[(int)value]+"x";
    }

    public void on_ras_change(float value){
        Label txt = (Label)GetNode("Settings/VBoxContainer/St_reflex_atlas_size/Txt");
        txt.Text = "Reflexion atlas size : "+atlas_sizes[(int)value];
    }

    public void on_sas_change(float value){
        Label txt = (Label)GetNode("Settings/VBoxContainer/St_shadow_atlas_size/Txt");
        txt.Text = "Shadow atlas size : "+atlas_sizes[(int)value];
    }

    public void on_ssc_change(float value){
        Label txt = (Label)GetNode("Settings/VBoxContainer/St_subsurface_scattering/Txt");
        txt.Text = "Subsurface Scattering : "+sscs[(int)value];
    }
    public void on_sao_change(float value){
        Label txt = (Label)GetNode("Settings/VBoxContainer/St_ambient_occlusion/Txt");
        txt.Text = "Ambient Occlusion : "+saos[(int)value];
    }

    public void on_cancel(){
        GetTree().ChangeScene("res://menus/MainMenu.tscn");
    }

    public void keep_settings(){
        popup.Hide();
        GetTree().ChangeScene("res://menus/Settings_graphics.tscn");
    }

    public void on_quit(){
        popup.Hide();
        GetTree().Quit();
    }

    public void _on_VScrollBar_value_changed(float value){
        vboxcontainere.RectPosition=new Vector2(0, -value*10);
    }

}
