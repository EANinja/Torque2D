/// <summary>
/// Call this function to scan a folder for images or sound files and create 
/// asset files for them.
/// </summary>
/// <param name="path">The path to scan - "^{EditorAssets}/data/images/".</param>
/// <param name="type">The asset type - "image" or "sound".</param>
/// <param name="category">The asset category.  If none, leave this blank.</param>
/// <param name="internal">Flag the asset as an internal asset.</param>
function makeAssetFiles(%path, %type, %category, %internal)
{
    switch$(%type)
    {
        case "image":
            %imageFile = findFirstFile(expandPath(%path @ "*.png"));
            while (%imageFile !$= "")
            {
                createAsset(%imageFile, %path, %type, %category, %internal);
                %imagefile = findNextFile(expandPath(%path @ "*.png"));
            }

        case "sound":
            %audioFile = findFirstFile(expandPath(%path @ "*.wav"));
            while (%audioFile !$= "")
            {
                createAsset(%audioFile, %path, %type, %category, %internal);
                %audioFile = findNextFile(expandPath(%path @ "*.wav"));
            }
    }
}


/// <summary>
/// This function creates an individual asset file for a particular image or sound.
/// </summary>
/// <param name="file">The file to create the asset for.</param>
/// <param name="path">The path to the file - "^{EditorAssets}/data/images/"</param>
/// <param name="type">The asset type - "image" or "sound"</param>
/// <param name="category">The asset category.  If none, leave this blank</param>
function createAsset(%file, %path, %type, %category, %internal)
{
    switch$(%type)
    {
        case "image":
            %newAsset = new ImageAsset();
            %newAsset.AssetName = fileBase(%file);
            %newAsset.AssetInternal = %internal;
            %newAsset.AssetAutoUnload = "0";
            %newAsset.ImageFile = %file;
            %newAsset.AssetCategory = %category;
            
        case "sound":
            %newAsset = new AudioAsset();
            %newAsset.AssetName = fileBase(%file);
            %newAsset.AssetInternal = %internal;
            %newAsset.AssetAutoUnload = "0";
            %newAsset.AudioFile = %file;
            %newAsset.AssetCategory = %category;
            
    }
    %assetFileName = expandPath(%path @ fileBase(%file) @ ".asset.taml");

    TamlWrite(%newAsset, %assetFileName);
}