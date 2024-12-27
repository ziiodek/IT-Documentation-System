window.triggerFileDownload = (fileName, url) => {
    const anchorElement = document.createElement('a');
    anchorElement.href = url;
    anchorElement.download = fileName ?? '';
    alert(fileName);
    anchorElement.click();
    anchorElement.remove();
    }
