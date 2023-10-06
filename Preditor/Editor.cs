using ImGuiNET;
using Microsoft.Xna.Framework;
using System.Linq;
using Num = System.Numerics; // vector 2 collision

namespace Preditor
{
    public class Editor
    {
        private Starbrite _engine;
        private bool _isRunning = false;
        private bool _showDemoWindow = false;

        public Editor(Starbrite engine) 
        { 
            _engine = engine;
        }

        public void Draw(GameTime gameTime)
        {
            ImGui.Begin(_engine.VersionString);

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
            ImGui.SeparatorText("Options (Read Only)");
            foreach (var option in _engine.Options)
            {
                if (option.Protected)
                {
                    ImGui.Text(option.Name + ": ");
                    ImGui.SameLine();

                    ImGui.SameLine();
                    ImGui.Text(_engine.GetOptionValue(option) + ": ");
                    ImGui.SameLine();
                    ImGui.Text(option.Description);
                }
            }

            ImGui.SeparatorText("Options (Writeable)");
            foreach (var option in _engine.Options) 
            { 
                if (!option.Protected) 
                { 
                    ImGui.Text(option.Name + ": ");
                    ImGui.SameLine();

                    ImGui.SameLine();
                    ImGui.Text(_engine.GetOptionValue(option) + ": ");
                    ImGui.SameLine();
                    ImGui.Text(option.Description);
                }
            }
        }

    }
}
