# Docker Setup

### NuGet Package
dotnet add package Microsoft.VisualStudio.Azure.Containers.Tools.Targets

### Docker
docker build -t ytdocker:v1 -f ProjectSetup/Dockerfile .
docker images
docker run -it --rm -p 8080:80 ytdocker:v1