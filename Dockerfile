# Этап 1: Используем образ для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Указываем рабочую директорию в контейнере
WORKDIR /app

# Копируем все файлы из текущей директории в контейнер
COPY . .

# Восстанавливаем зависимости
RUN dotnet restore "Itransition.Trainee.Web.csproj"

# Собираем проект в режиме Release
RUN dotnet publish "Itransition.Trainee.Web.csproj" -c Release -o out

# Этап 2: Используем образ для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

WORKDIR /app

# Копируем собранный проект из build
COPY --from=build /app/out .

# Устанавливаем команду для старта приложения
ENTRYPOINT ["dotnet", "Itransition.Trainee.Web.dll"]
