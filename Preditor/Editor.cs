using ImGuiNET;
using Microsoft.Xna.Framework;
using System.Linq;

namespace Preditor
{
    public class Editor
    {
        private Starbrite _engine;
        private bool _isRunning = false;
        private bool _showDemoWindow = false;

        public bool IsRunning => _isRunning;

        public Editor(Starbrite engine) 
        { 
            _engine = engine;
            _isRunning = true;
        }

        public void Draw(GameTime gameTime)
        {
            if (ImGui.Button("Power " + (_isRunning ? "Off": "On"))) Toggle("isRunning");
            if (ImGui.Button("Demo " + (_showDemoWindow ? "Off" : "On"))) Toggle("showDemo");
            if (ImGui.Button("List Config Files")) _engine.ListConfigFiles(); // get file list

            if (_engine.ConfigFileArray.Count() > 0)
            {
                if (ImGui.Button("Scan " + _engine.ConfigFileArray.Count().ToString() + " Config Files")) _engine.ScanConfigFiles(); // scan file content
            }

            ImGui.Text(string.Format("Application average {0:F3} ms/frame ({1:F1} FPS)", 1000f / ImGui.GetIO().Framerate, ImGui.GetIO().Framerate));


            if (_showDemoWindow)
            {
                ImGui.ShowDemoWindow();
            }

            ShowOptionsTable();
        }

        ///// IMGUI Functions - call from Draw()
        public void ShowOptionsTable()
        {

            ImGui.SeparatorText("Options");
            foreach (var option in _engine.Options) 
            { 
                ImGui.Text(option.Name + ": "); 
                ImGui.SameLine();
                ImGui.Text(GetOptionValue(option));
            }
        }

        public string GetOptionValue(StarbriteOption _option)
        {
            switch (_option.Type)
            {
                case "string":
                    var os = (StarbriteOptionString)_option;
                    return os.Value;
                case "int":
                    var oi = (StarbriteOptionInt)_option;
                    return oi.Value.ToString();
            }

            return "ERROR: GetOptionValue";
        }

        // get rid of this shit
        public bool Toggle(string setting)
        {
            switch (setting)
            {
                case "isRunning":
                    _isRunning = !_isRunning;
                    return _isRunning;
                case "showDemo":
                    _showDemoWindow = !_showDemoWindow;
                    return _showDemoWindow;
            }

            return false;
        }
    }
}
