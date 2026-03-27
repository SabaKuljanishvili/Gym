# Build Stage - ვიყენებთ .NET 9.0 SDK-ს
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# ვაკოპირებთ ყველა .csproj ფაილს (DAL-ის ჩათვლით)
COPY ["GymMembershipManagement.API/GymMembershipManagement.API.csproj", "GymMembershipManagement.API/"]
COPY ["GymMembershipManagement.DATA/GymMembershipManagement.DATA.csproj", "GymMembershipManagement.DATA/"]
COPY ["GymMembershipManagement.SERVICE/GymMembershipManagement.SERVICE.csproj", "GymMembershipManagement.SERVICE/"]
# აუცილებლად დაამატე DAL, თუ პროექტი მას იყენებს:
COPY ["GymMembershipManagement.DAL/GymMembershipManagement.DAL.csproj", "GymMembershipManagement.DAL/"]

# ვაკეთებთ Restore-ს
RUN dotnet restore "GymMembershipManagement.API/GymMembershipManagement.API.csproj"

# ვაკოპირებთ მთლიან კოდს
COPY . .
WORKDIR "/src/GymMembershipManagement.API"

# ვაშენებთ პროექტს
RUN dotnet publish "GymMembershipManagement.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Runtime Stage - ვიყენებთ .NET 9.0 Runtime-ს
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .
ENTRYPOINT ["dotnet", "GymMembershipManagement.API.dll"]