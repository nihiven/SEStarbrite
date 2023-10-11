using ImGuiNET;
using Microsoft.Xna.Framework;
using System.Linq;
using Num = System.Numerics; // vector 2 collision

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

            ImGui.SeparatorText("System Info & Operations");

            if (ImGui.Button("Demo " + (_showDemoWindow ? "Off" : "On"))) _showDemoWindow = !_showDemoWindow;
            if (ImGui.Button("List Config Files")) _engine.ListConfigFiles(); // get file list

            if (_engine.ConfigFileArray.Count() > 0)
            {
                if (ImGui.Button("Scan " + _engine.ConfigFileArray.Count().ToString() + " Config Files")) _engine.ScanConfigFiles(); // scan file content
            }

            ImGui.Text(string.Format("Frame Time: {0:F3}ms / {1:F1} FPS", 1000f / ImGui.GetIO().Framerate, ImGui.GetIO().Framerate));

            ShowOptionsTable();

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
            var _textWidth = ImGui.CalcTextSize("A").X;
            var _textHeight = ImGui.GetTextLineHeightWithSpacing();

            // ??
            //ImGui.PushID("Baby Baby");
            var _tableFlags = ImGuiTableFlags.Borders | ImGuiTableFlags.RowBg | ImGuiTableFlags.Reorderable | ImGuiTableFlags.RowBg | ImGuiTableFlags.SizingFixedFit | ImGuiTableFlags.NoHostExtendX;

            // colors
            var _colorDefault = ImGui.GetColorU32(new Num.Vector4(0.16f, 0.16f, 0.3f, 1f));
            var _colorEditNotSaved = ImGui.GetColorU32(new Num.Vector4(0.0f, 0.5f, 0.0f, 1f));
            var _colorEditSaved = ImGui.GetColorU32(new Num.Vector4(0.0f, 0.0f, 0.5f, 1f));

            ImGui.BeginTable("System", 7, _tableFlags);

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
            ImGui.TableSetColumnIndex(6);
            ImGui.Text("Edit");

            //-------
            foreach (var option in _engine.OptionStore.Options)
            {
                var _value = _engine.GetOptionValue(option);
                var _valueDefault = _engine.GetOptionValueDefault(option);

                ImGui.TableNextRow();
                ImGui.TableSetColumnIndex(0);
                ImGui.Text(option.Name);
                ImGui.TableSetColumnIndex(1);
                ImGui.Text(_value);
                if (_value != _valueDefault) 
                {
                    ImGui.TableSetBgColor(ImGuiTableBgTarget.CellBg, _colorDefault, 2);
                }
                ImGui.TableSetColumnIndex(2);
                ImGui.Text(_valueDefault);
                ImGui.TableSetColumnIndex(3);
                ImGui.Text(option.Description);
                ImGui.TableSetColumnIndex(4);
                ImGui.Text(option.Type);
                ImGui.TableSetColumnIndex(5);
                ImGui.Text(option.Protected.ToString());
                ImGui.TableSetColumnIndex(6);
                ImGui.Text("Edit");
            }


            ImGui.EndTable();   


        }

    }
}
