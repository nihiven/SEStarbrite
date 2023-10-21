using Serilog;

Log.Debug("Program startup");
using var game = new Preditor.Host();
game.Run();
Log.Debug("Program shutdown");
