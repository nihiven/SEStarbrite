using ImGuiNET;
using Microsoft.Xna.Framework;
using System.Linq;

namespace Preditor
{
  

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
                    ImGui.TableSetBgColor(ImGuiTableBgTarget.CellBg, ImGuiColors.LightPink, 1);
                }
            }


            ImGui.EndTable();   


        }

    }
}
