using System.Linq;
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
    public bool need_restart=false;
    public bool at_start=true;


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
        //
        popup = (PopupDialog)GetNode("PopupDialog");
        vscrollbare = (VScrollBar)GetNode("VScrollBar");
        vboxcontainere = (VBoxContainer)GetNode("Settings/VBoxContainer");
        //Initialisation des Layout settings
        //fps
        cb_showfps.Pressed=globale.aff_fps;
        //resolution
        te_width.Text=""+ProjectSettings.GetSetting("display/window/size/width");
        te_height.Text=""+ProjectSettings.GetSetting("display/window/size/height");
        //vsync
        CheckBox cb_vsync = (CheckBox)GetNode("Settings/VBoxContainer/St_vsync/Cb");
        cb_vsync.Pressed=(Boolean)ProjectSettings.GetSetting("display/window/vsync/use_vsync");
        //3d effects
        CheckBox cb_3de = (CheckBox)GetNode("Settings/VBoxContainer/St_3d_effects/Cb");
        int fba;
        if(globale.is_mobile()){
            fba = (int)ProjectSettings.GetSetting("rendering/quality/intended_usage/framebuffer_allocation.mobile");
        }
        else{
            fba =(int)ProjectSettings.GetSetting("rendering/quality/intended_usage/framebuffer_allocation");
        }
        cb_3de.Pressed = fba==2;
        //hdr
        CheckBox cb_hdr = (CheckBox)GetNode("Settings/VBoxContainer/St_hdr/Cb");
        if(globale.is_mobile()){
            cb_hdr.Pressed=(Boolean)ProjectSettings.GetSetting("rendering/quality/depth/hdr.mobile");
        }
        else{
            cb_hdr.Pressed=(Boolean)ProjectSettings.GetSetting("rendering/quality/depth/hdr");
        }
        //anisotropic
        HScrollBar hsb_anis = (HScrollBar)GetNode("Settings/VBoxContainer/St_anisotropic/HScrollBar");
        hsb_anis.MaxValue = anisotropics.Length;
        int aniv = (int)ProjectSettings.GetSetting("rendering/quality/filters/anisotropic_filter_level");
        if(anisotropics.Contains(aniv)){
            hsb_anis.Value=Array.IndexOf(anisotropics,aniv);
        }
        else{
            hsb_anis.Value=0;
            ProjectSettings.SetSetting("rendering/quality/filters/anisotropic_filter_level", anisotropics[0]);
        }
        on_anis_change((float)hsb_anis.Value);
        //High quality ggx
        CheckBox cb_ggx = (CheckBox)GetNode("Settings/VBoxContainer/St_high_quality_ggx/Cb");
        if( globale.is_mobile() ){
            cb_ggx.Pressed=(Boolean)ProjectSettings.GetSetting("rendering/quality/reflections/high_quality_ggx.mobile");
        }
        else{
            cb_ggx.Pressed=(Boolean)ProjectSettings.GetSetting("rendering/quality/reflections/high_quality_ggx");
        }  
        //Reflex Atlas Size
        HScrollBar hsb_ras = (HScrollBar)GetNode("Settings/VBoxContainer/St_reflex_atlas_size/HScrollBar");
        hsb_ras.MaxValue = atlas_sizes.Length;
        int rasv = (int)ProjectSettings.GetSetting("rendering/quality/reflections/atlas_size");
        if(atlas_sizes.Contains(rasv)){
            hsb_ras.Value=Array.IndexOf(atlas_sizes,rasv);
        }
        else{
            hsb_ras.Value=0;
            ProjectSettings.SetSetting("rendering/quality/reflections/atlas_size", atlas_sizes[0]);
        }
        on_ras_change((float)hsb_ras.Value);
        //texture array reflexion
        CheckBox cb_tar = (CheckBox)GetNode("Settings/VBoxContainer/St_high_quality_ggx/Cb");
        if( globale.is_mobile() ){
            cb_tar.Pressed=(Boolean)ProjectSettings.GetSetting("rendering/quality/reflections/texture_array_reflections.mobile");
        }
        else{
            cb_tar.Pressed=(Boolean)ProjectSettings.GetSetting("rendering/quality/reflections/texture_array_reflections");
        }
        //Force vertex Shading
        CheckBox cb_fvs = (CheckBox)GetNode("Settings/VBoxContainer/St_force_vertex_shading/Cb");
        if( globale.is_mobile() ){
            cb_fvs.Pressed=(Boolean)ProjectSettings.GetSetting("rendering/quality/shading/force_vertex_shading.mobile");
        }
        else{
            cb_fvs.Pressed=(Boolean)ProjectSettings.GetSetting("rendering/quality/shading/force_vertex_shading");
        }
        //Shadow Atlas size
        HScrollBar hsb_sas = (HScrollBar)GetNode("Settings/VBoxContainer/St_shadow_atlas_size/HScrollBar");
        hsb_sas.MaxValue = atlas_sizes.Length;
        string path_sas = "rendering/quality/shadow_atlas/size";
        if(globale.is_mobile()){ path_sas="rendering/quality/shadow_atlas/size.mobile"; }
        int sasv = (int)ProjectSettings.GetSetting(path_sas);
        if(atlas_sizes.Contains(sasv)){
            hsb_sas.Value=Array.IndexOf(atlas_sizes,sasv);
        }
        else{
            hsb_sas.Value=0;
            ProjectSettings.SetSetting(path_sas, atlas_sizes[0]);
        }
        on_sas_change((float)hsb_sas.Value);
        //Subsurface scattering
        HScrollBar hsb_ssc = (HScrollBar)GetNode("Settings/VBoxContainer/St_subsurface_scattering/HScrollBar");
        hsb_ssc.MaxValue=3;
        hsb_ssc.Value=(int)ProjectSettings.GetSetting("rendering/quality/subsurface_scattering/quality");
        on_ssc_change((float)hsb_ssc.Value);
        //Ambient occlusion
        HScrollBar hsb_sao = (HScrollBar)GetNode("Settings/VBoxContainer/St_ambient_occlusion/HScrollBar");
        hsb_sao.MaxValue=saos.Length;
        if(globale.sao==true){
            hsb_sao.Value=0;
        }
        else{
            hsb_sao.Value=globale.sao_quality+1;
        }
        on_sao_change((float)hsb_sao.Value);
        //Glow Strength
        HScrollBar hsb_gs = (HScrollBar)GetNode("Settings/VBoxContainer/St_glow_strength/HScrollBar");
        hsb_gs.Value=globale.glow_strength;
        on_glow_s_change((float)hsb_gs.Value);
        //Glow Intensity
        HScrollBar hsb_gi = (HScrollBar)GetNode("Settings/VBoxContainer/St_glow_intensity/HScrollBar");
        hsb_gi.Value=globale.glow_intensity;
        on_glow_i_change((float)hsb_gi.Value);
        //Saturation
        HScrollBar hsb_sat = (HScrollBar)GetNode("Settings/VBoxContainer/St_Saturation/HScrollBar");
        hsb_sat.Value=globale.saturation;
        on_saturation_change((float)hsb_sat.Value);
        //Brightness
        HScrollBar hsb_lum = (HScrollBar)GetNode("Settings/VBoxContainer/St_Brightness/HScrollBar");
        hsb_lum.Value=globale.luminosity;
        on_brightness_change((float)hsb_lum.Value);
        //Far
        HScrollBar hsb_far = (HScrollBar)GetNode("Settings/VBoxContainer/St_fov/HScrollBar");
        hsb_far.Value=globale.cam_far;
        on_far_change((float)hsb_far.Value);
        //
        at_start=false;
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
        if(width!=AncientSettings.width || height!=AncientSettings.height){
            need_restart=true;
        }
        //FPS
        globale.aff_fps=cb_showfps.Pressed;
        //Resolution
        ProjectSettings.SetSetting("display/window/size/width", (int)width);
        ProjectSettings.SetSetting("display/window/size/height", (int)height);
        //Vsync
        CheckBox cb_vsync = (CheckBox)GetNode("Settings/VBoxContainer/St_vsync/Cb");
        if(cb_vsync.Pressed!=(bool)ProjectSettings.GetSetting("display/window/vsync/use_vsync")){
            need_restart=true;
            ProjectSettings.SetSetting("display/window/vsync/use_vsync",cb_vsync.Pressed);
        }
        //3d effects
        CheckBox cb_effects = (CheckBox)GetNode("Settings/VBoxContainer/St_3d_effects/Cb");
        int value = 3;
        if(cb_effects.Pressed){ value = 2;}
        if( globale.is_mobile() ){
            if(value!=(int)ProjectSettings.GetSetting("rendering/quality/intended_usage/framebuffer_allocation.mobile")){
                need_restart=true;
                ProjectSettings.SetSetting("rendering/quality/intended_usage/framebuffer_allocation.mobile",value);
            }
        }
        else{
            if(value!=(int)ProjectSettings.GetSetting("rendering/quality/intended_usage/framebuffer_allocation")){
                need_restart=true;
                ProjectSettings.SetSetting("rendering/quality/intended_usage/framebuffer_allocation",value);
            }
        }        
        //hdr
        CheckBox cb_hdr = (CheckBox)GetNode("Settings/VBoxContainer/St_hdr/Cb");
        if( globale.is_mobile() ){
            if(cb_hdr.Pressed!=(bool)ProjectSettings.GetSetting("rendering/quality/depth/hdr.mobile")){
                ProjectSettings.SetSetting("rendering/quality/depth/hdr.mobile",cb_hdr.Pressed);
                need_restart=true;
            }
        }
        else{
            if(cb_hdr.Pressed!=(bool)ProjectSettings.GetSetting("rendering/quality/depth/hdr")){
                ProjectSettings.SetSetting("rendering/quality/depth/hdr",cb_hdr.Pressed);
                need_restart=true;
            }
        }   
        //Anisotropic
        HScrollBar sb_ani = (HScrollBar)GetNode("Settings/VBoxContainer/St_anisotropic/HScrollBar");
        int value_ani = anisotropics[(int)sb_ani.Value];
        ProjectSettings.SetSetting("rendering/quality/filters/anisotropic_filter_level", value_ani);
        //high quality ggx
        CheckBox cb_ggx = (CheckBox)GetNode("Settings/VBoxContainer/St_high_quality_ggx/Cb");
        if( globale.is_mobile() ){
            if(cb_ggx.Pressed!=(bool)ProjectSettings.GetSetting("rendering/quality/reflections/high_quality_ggx.mobile")){
                need_restart=true;
                ProjectSettings.SetSetting("rendering/quality/reflections/high_quality_ggx.mobile",cb_ggx.Pressed);
            }
        }
        else{
            if(cb_ggx.Pressed!=(bool)ProjectSettings.GetSetting("rendering/quality/reflections/high_quality_ggx")){
                need_restart=true;
                ProjectSettings.SetSetting("rendering/quality/reflections/high_quality_ggx",cb_ggx.Pressed);
            }
        }  
        //Reflexion Atlas Size
        HScrollBar sb_ras = (HScrollBar)GetNode("Settings/VBoxContainer/St_reflex_atlas_size/HScrollBar");
        int value_ras = atlas_sizes[(int)sb_ras.Value];
        ProjectSettings.SetSetting("rendering/quality/reflections/atlas_size", value_ras);
        //Texture array reflexion
        CheckBox cb_tar = (CheckBox)GetNode("Settings/VBoxContainer/St_high_quality_ggx/Cb");
        if( globale.is_mobile() ){
            if(cb_tar.Pressed!=(bool)ProjectSettings.GetSetting("rendering/quality/reflections/texture_array_reflections.mobile")){
                need_restart=true;
                ProjectSettings.SetSetting("rendering/quality/reflections/texture_array_reflections.mobile",cb_tar.Pressed);
            }
        }
        else{
            if(cb_tar.Pressed!=(bool)ProjectSettings.GetSetting("rendering/quality/reflections/texture_array_reflections")){
                need_restart=true;
                ProjectSettings.SetSetting("rendering/quality/reflections/texture_array_reflections",cb_tar.Pressed);
            }
        }
        //Force vertex shading
        CheckBox cb_fvs = (CheckBox)GetNode("Settings/VBoxContainer/St_force_vertex_shading/Cb");
        if( globale.is_mobile() ){
            if(cb_fvs.Pressed!=(bool)ProjectSettings.GetSetting("rendering/quality/shading/force_vertex_shading.mobile")){
                need_restart=true;
                ProjectSettings.SetSetting("rendering/quality/shading/force_vertex_shading.mobile",cb_fvs.Pressed);
            }
        }
        else{
            if(cb_fvs.Pressed!=(bool)ProjectSettings.GetSetting("rendering/quality/shading/force_vertex_shading")){
                need_restart=true;
                ProjectSettings.SetSetting("rendering/quality/shading/force_vertex_shading",cb_fvs.Pressed);
            }
        }
        //Shadow Atlas size
        HScrollBar sb_sas = (HScrollBar)GetNode("Settings/VBoxContainer/St_reflex_atlas_size/HScrollBar");
        int value_sas = atlas_sizes[(int)sb_sas.Value];
        if(globale.is_mobile()){
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
        //Glow Strength
        HScrollBar hsb_gs = (HScrollBar)GetNode("Settings/VBoxContainer/St_glow_strength/HScrollBar");
        globale.glow_strength=(float)hsb_gs.Value;
        //Glow Intensity
        HScrollBar hsb_gi = (HScrollBar)GetNode("Settings/VBoxContainer/St_glow_intensity/HScrollBar");
        globale.glow_intensity=(float)hsb_gi.Value;
        //Saturation
        HScrollBar hsb_sat = (HScrollBar)GetNode("Settings/VBoxContainer/St_Saturation/HScrollBar");
        globale.saturation=(float)hsb_sat.Value;
        //Brightness
        HScrollBar hsb_lum = (HScrollBar)GetNode("Settings/VBoxContainer/St_Brightness/HScrollBar");
        globale.luminosity=(float)hsb_lum.Value;
        //Far
        HScrollBar hsb_far = (HScrollBar)GetNode("Settings/VBoxContainer/St_fov/HScrollBar");
        globale.cam_far = (float)hsb_far.Value;
        //Save Settings
        ProjectSettings.Save();
        globale.SaveGame();
        //
        if(need_restart){
            popup.Popup_();
        }
    }

    public void on_anis_change(float value){
        Label txt = (Label)GetNode("Settings/VBoxContainer/St_anisotropic/Txt");
        txt.Text = "Anisotropic Filter Level : "+anisotropics[(int)value]+"x";
        if(!at_start){
            need_restart=true;
        }
    }

    public void on_ras_change(float value){
        Label txt = (Label)GetNode("Settings/VBoxContainer/St_reflex_atlas_size/Txt");
        txt.Text = "Reflexion atlas size : "+atlas_sizes[(int)value];
        if(!at_start){
            need_restart=true;
        }
    }

    public void on_sas_change(float value){
        Label txt = (Label)GetNode("Settings/VBoxContainer/St_shadow_atlas_size/Txt");
        txt.Text = "Shadow atlas size : "+atlas_sizes[(int)value];
        if(!at_start){
            need_restart=true;
        }
    }

    public void on_ssc_change(float value){
        Label txt = (Label)GetNode("Settings/VBoxContainer/St_subsurface_scattering/Txt");
        txt.Text = "Subsurface Scattering : "+sscs[(int)value];
        if(!at_start){
            need_restart=true;
        }
    }
    public void on_sao_change(float value){
        Label txt = (Label)GetNode("Settings/VBoxContainer/St_ambient_occlusion/Txt");
        txt.Text = "Ambient Occlusion : "+saos[(int)value];
    }

    public void on_glow_s_change(float value){
        Label txt = (Label)GetNode("Settings/VBoxContainer/St_glow_strength/Txt");
        txt.Text="Glow Strength : "+value;
    }

    public void on_glow_i_change(float value){
        Label txt = (Label)GetNode("Settings/VBoxContainer/St_glow_intensity/Txt");
        txt.Text="Glow Intensity : "+value;
    }

    public void on_saturation_change(float value){
        Label txt = (Label)GetNode("Settings/VBoxContainer/St_Saturation/Txt");
        txt.Text="Saturation : "+value;
    }

    public void on_brightness_change(float value){
        Label txt = (Label)GetNode("Settings/VBoxContainer/St_Brightness/Txt");
        txt.Text="Brightness : "+value;
    }

    public void on_far_change(float value){
        Label txt = (Label)GetNode("Settings/VBoxContainer/St_fov/Txt");
        txt.Text="Max camera view distance : "+value;
    }

    public void on_default_glow_s(){
        HScrollBar hsb_g_s = (HScrollBar)GetNode("Settings/VBoxContainer/St_glow_strength/HScrollBar");
        hsb_g_s.Value=0.96;
        on_glow_s_change(0.96F);
    }

    public void on_default_glow_i(){
        HScrollBar hsb_g_i = (HScrollBar)GetNode("Settings/VBoxContainer/St_glow_intensity/HScrollBar");
        hsb_g_i.Value=1;
        on_glow_i_change(1);
    }

    public void on_default_lum(){
        HScrollBar hsb_lum = (HScrollBar)GetNode("Settings/VBoxContainer/St_Brightness/HScrollBar");
        hsb_lum.Value=1;
        on_brightness_change(1);
    }

    public void on_default_sat(){
        HScrollBar hsb_sat = (HScrollBar)GetNode("Settings/VBoxContainer/St_Saturation/HScrollBar");
        hsb_sat.Value=1;
        on_saturation_change(1);
    }
    
    public void on_default_far(){
        HScrollBar hsb_far = (HScrollBar)GetNode("Settings/VBoxContainer/St_fov/HScrollBar");
        hsb_far.Value=100;
        on_far_change(100);
    }

    public void on_cancel(){
        GetTree().ChangeScene("res://menus/MainMenu.tscn");
    }

    public void keep_settings(){
        popup.Hide();
        GetTree().Quit();        
    }

    public void on_quit(){
        popup.Hide();
        GetTree().ChangeScene("res://menus/Settings_graphics.tscn");
    }

    public void _on_VScrollBar_value_changed(float value){
        vboxcontainere.RectPosition=new Vector2(0, -value*10);
    }

}
