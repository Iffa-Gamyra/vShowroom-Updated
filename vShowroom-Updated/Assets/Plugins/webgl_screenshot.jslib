mergeInto(LibraryManager.library, {
    DownloadScreenshot: function(base64) {
        var base64Str = UTF8ToString(base64);
        var link = document.createElement('a');
        link.href = 'data:image/png;base64,' + base64Str;
        link.download = 'screenshot.png';
        link.click();
    }
});