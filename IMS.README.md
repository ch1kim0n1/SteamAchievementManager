# Inventory Management System (IMS)

A local-only inventory and currency management tool modeled after Steam Achievement Manager's architecture.

## Structure

- **IMS.Picker** - Profile selection launcher (similar to SAM.Picker)
- **IMS.Manager** - Main inventory/currency manager (similar to SAM.Game)

## How to Use

1. Open SAM.sln in Visual Studio
2. Set "IMS.Picker" as the startup project
3. Build and run (Debug|x86)

The picker will show available profiles and launch the manager when you select one.

## Architecture

- **Picker**: Lists saved profiles, allows creating/deleting profiles, launches manager
- **Manager**: Manages items and currencies for a specific profile, auto-saves changes
- **Storage**: JSON files in `%APPDATA%\InventoryManagementSystem\profiles\`

## Features

- Profile-based organization (like SAM's game selection)
- Tabbed interface for Items and Currencies
- Toolbar buttons for add/remove/reload/store operations
- Status bar showing current state
- Auto-save functionality
- Import/Export capabilities

## Safety

This tool is completely local and does not interact with Steam, games, or any online services. It's designed for prototyping, learning, and local data management only.
