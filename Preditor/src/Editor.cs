using ImGuiNET;
using Microsoft.Xna.Framework;
using System.Linq;
using Num = System.Numerics; // vector2 collision

namespace Preditor
{
    class ImGuiColors
    {
        public static uint Red => ImGui.GetColorU32(new Num.Vector4(1.0f, 0.0f, 0.0f, 1f));
        public static uint Blue => ImGui.GetColorU32(new Num.Vector4(0.0f, 0.0f, 1.0f, 1f));
        public static uint Green => ImGui.GetColorU32(new Num.Vector4(0.0f, 1.0f, 0.0f, 1f));
        public static uint Yellow => ImGui.GetColorU32(new Num.Vector4(1.0f, 1.0f, 0.0f, 1f));
        public static uint Orange => ImGui.GetColorU32(new Num.Vector4(1.0f, 0.5f, 0.0f, 1f));
        public static uint Purple => ImGui.GetColorU32(new Num.Vector4(0.5f, 0.0f, 1.0f, 1f));
        public static uint Cyan => ImGui.GetColorU32(new Num.Vector4(0.0f, 1.0f, 1.0f, 1f));
        public static uint Magenta => ImGui.GetColorU32(new Num.Vector4(1.0f, 0.0f, 1.0f, 1f));
        public static uint White => ImGui.GetColorU32(new Num.Vector4(1.0f, 1.0f, 1.0f, 1f));
        public static uint Black => ImGui.GetColorU32(new Num.Vector4(0.0f, 0.0f, 0.0f, 1f));

        public static uint LightRed => ImGui.GetColorU32(new Num.Vector4(1.0f, 0.0f, 0.0f, 0.25f));
        public static uint LightBlue => ImGui.GetColorU32(new Num.Vector4(0.0f, 0.0f, 1.0f, 0.25f));
        public static uint LightGreen => ImGui.GetColorU32(new Num.Vector4(0.0f, 1.0f, 0.0f, 0.25f));
        public static uint LightYellow => ImGui.GetColorU32(new Num.Vector4(1.0f, 1.0f, 0.0f, 0.25f));
        public static uint LightOrange => ImGui.GetColorU32(new Num.Vector4(1.0f, 0.5f, 0.0f, 0.25f));
        public static uint LightPurple => ImGui.GetColorU32(new Num.Vector4(0.5f, 0.0f, 1.0f, 0.25f));
        public static uint LightCyan => ImGui.GetColorU32(new Num.Vector4(0.0f, 1.0f, 1.0f, 0.25f));
        public static uint LightMagenta => ImGui.GetColorU32(new Num.Vector4(1.0f, 0.0f, 1.0f, 0.25f));
        public static uint LightWhite => ImGui.GetColorU32(new Num.Vector4(1.0f, 1.0f, 1.0f, 0.25f));
        public static uint LightBlack => ImGui.GetColorU32(new Num.Vector4(0.0f, 0.0f, 0.0f, 0.25f));

    }

    public class Editor
    {
        private Starbrite _engine;
        private bool _showDemoWindow = false;

        public Editor(Starbrite engine) 
        { 
            _engine = engine;
        }

        public void Draw(GameTime gameTime)
        {
            ImGui.Begin("System");

            // OPTIONS
            ImGui.SeparatorText("Option Variables");
            ShowOptionsTable();


            // DEBUG
            ImGui.SeparatorText("Debug");

            if (ImGui.Button("Demo " + (_showDemoWindow ? "Off" : "On"))) _showDemoWindow = !_showDemoWindow;
            if (ImGui.Button("List Config Files")) _engine.ListConfigFiles(); // get file list

            if (_engine.ConfigFileArray.Count() > 0)
            {
                if (ImGui.Button("Scan " + _engine.ConfigFileArray.Count().ToString() + " Config Files")) _engine.ScanConfigFiles(); // scan file content
            }

            ImGui.Text(string.Format("Frame Time: {0:F3}ms / {1:F1} FPS", 1000f / ImGui.GetIO().Framerate, ImGui.GetIO().Framerate));

            // imgui included demo
            if (_showDemoWindow)
            {
                ImGui.ShowDemoWindow();
            }

            ImGui.End();
        }

        ///// IMGUI Functions - call from Draw()
        public void ShowOptionsTable()
        {
            // keep these in mind
            float _textWidth = ImGui.CalcTextSize("A").X;
            float _textHeight = ImGui.GetTextLineHeightWithSpacing();

            // ??
            //ImGui.PushID("Baby Baby");
            ImGuiTableFlags _tableFlags = ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.Reorderable | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.NoHostExtendX;

            ImGui.BeginTable("System", 6, _tableFlags);



            // table header
            ImGui.TableHeadersRow();
            ImGui.TableSetColumnIndex(0);
            ImGui.Text("Name");
            ImGui.TableSetColumnIndex(1);
            ImGui.Text("Value");
            ImGui.TableSetColumnIndex(2);
            ImGui.Text("Default Value");
            ImGui.TableSetColumnIndex(3);
            ImGui.Text("Description");
            ImGui.TableSetColumnIndex(4);
            ImGui.Text("Type");
            ImGui.TableSetColumnIndex(5);
            ImGui.Text("Read Only");
            //ImGui.TableSetColumnIndex(6);
            //ImGui.Text("Edit");

            //-------
            foreach (StarbriteOption option in _engine.OptionStore.Options)
            {
                string _value = _engine.GetOptionValue(option);
                string _valueDefault = _engine.GetOptionValueDefault(option);

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text(option.Name);

                // make the value editable
                ImGui.TableSetColumnIndex(1);
                ImGui.Text(_value);


                ImGui.TableSetColumnIndex(2);
                ImGui.Text(_valueDefault);
                ImGui.TableSetColumnIndex(3);
                ImGui.Text(option.Description);
                ImGui.TableSetColumnIndex(4);
                ImGui.Text(option.Type);
                ImGui.TableSetColumnIndex(5);
                ImGui.Text(option.Protected.ToString());
                
                //ImGui.TableSetColumnIndex(6);

                // lets get some text editing!
                //ImGui.Text("Edit");

                // change cell colors
                if (_value != _valueDefault)
                {
                    ImGui.TableSetBgColor(ImGuiTableBgTarget.CellBg, ImGuiColors.LightGreen, 1);
                }
            }


            ImGui.EndTable();   


        }

    }
}
