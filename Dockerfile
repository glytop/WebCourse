# Этап 1: Используем образ для сборки приложения
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

# Указываем рабочую директорию в контейнере
WORKDIR /app

# Копируем все файлы из текущей директории в контейнер
COPY . .

# Восстанавливаем зависимости
RUN dotnet restore "MyApp/MyApp.csproj"

# Собираем проект в режиме Release
RUN dotnet publish "MyApp/MyApp.csproj" -c Release -o /app/publish

# Этап 2: Используем образ для запуска приложения
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

# Указываем рабочую директорию в контейнере
WORKDIR /app

# Копируем опубликованные файлы из предыдущего этапа
COPY --from=build /app/publish .

# Открываем порт 80 для контейнера
EXPOSE 80

# Указываем команду для запуска приложения
ENTRYPOINT ["dotnet", "MyApp.dll"]
