# Paths
$packFolder = (Get-Item -Path "./" -Verbose).FullName
$rootFolder = Join-Path $packFolder "../"

# List of solutions
$solutions = (
    ""
)

# List of projects
$projects = (
	"src/X.Abp.StrainerPipe.Core",
    "src/X.Abp.StrainerPipe.Channel",
    "src/X.Abp.StrainerPipe.Channel.Transfer",
    "src/X.Abp.StrainerPipe.Sink",
    "src/X.Abp.StrainerPipe.Source",
    "src/X.Abp.StrainerPipe.Source.MqttNetServer"
)