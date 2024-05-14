using Cocona;
using Cocona.Builder;
using TheLair.CLI.Commands;

CoconaAppBuilder builder = CoconaApp.CreateBuilder();
CoconaApp app = builder.Build();

app.AddCommands<CreateCommand>();

app.Run();