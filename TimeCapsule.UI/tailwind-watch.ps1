if (-not (Get-Command bun -ErrorAction SilentlyContinue)) {
    Write-Host "Error: Bun is not installed." -ForegroundColor Red
    Write-Host "Please install it by running: powershell -c 'irm bun.sh/install.ps1 | iex'"
    exit 1
}

Write-Host "Bun detected. Starting Tailwind watcher..." -ForegroundColor Green

bunx @tailwindcss/cli -i ./Styles/input.css -o ./wwwroot/css/tailwind.css --watch