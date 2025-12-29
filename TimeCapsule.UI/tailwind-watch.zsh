#!/bin/zsh

# Check if Bun is installed
if ! command -v bun &> /dev/null; then
    echo "\033[0;31mError: Bun is not installed.\033[0m"
    echo "Please install it first: curl -fsSL https://bun.sh/install | bash"
    exit 1
fi

echo "\033[0;32mBun detected. Starting Tailwind watcher...\033[0m"

# Run the command
bunx @tailwindcss/cli -i ./Styles/input.css -o ./wwwroot/css/tailwind.css --watch