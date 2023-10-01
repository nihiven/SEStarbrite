using ImGuiNET;
using Microsoft.Xna.Framework;
using System.Linq;

namespace Preditor
{
    public class Editor
    {
        private Starbrite _engine;
        private bool _isRunning = false;

        public bool IsRunning => _isRunning;

        public Editor(Starbrite engine) 
        { 
            _engine = engine;
            _isRunning = true;
        }

        public void Draw(GameTime gameTime)
        {
            if (ImGui.Button("Power " + (_isRunning ? "Off": "On"))) Toggle("isRunning");

            if (ImGui.Button("List Config Files")) _engine.ListConfigFiles(); // get file list

            if (_engine.ConfigFileArray.Count() > 0)
            {
                if (ImGui.Button("Scan Config Files")) _engine.ScanConfigFiles(); // scan file content
            }

            ImGui.Text(string.Format("Application average {0:F3} ms/frame ({1:F1} FPS)", 1000f / ImGui.GetIO().Framerate, ImGui.GetIO().Framerate));

            ImGui.Text(_engine.ConfigFileArray.Count().ToString());
        }

        public bool Toggle(string setting)
        {
            switch (setting)
            {
                case "isRunning":
                    _isRunning = !_isRunning;
                    return _isRunning;
            }

            return false;
        }
    }
}
