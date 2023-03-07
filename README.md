# AssetTracker
AssetTracker is a simple application that allows customers to list all their high-value items and calculate the total content limits required in case of an insurance claim.

# Before the first run
1. Open the `Appsettings.json` file and update the `Data source` in the connection string. (currently set to `"."`).
2. Open the `Program.cs` file and pass in the same `Data source` (currently passing `"."`) into the SetupDatabase method.
3. The database should have some data after the first successful run. 

## This is how the UI will look after the first run

<img src="https://user-images.githubusercontent.com/57675296/223298151-0c8ea7b5-deba-4704-9027-668ef61a7904.jpg" data-canonical-src="https://user-images.githubusercontent.com/57675296/223298151-0c8ea7b5-deba-4704-9027-668ef61a7904.jpg" width="500" height="400" />
