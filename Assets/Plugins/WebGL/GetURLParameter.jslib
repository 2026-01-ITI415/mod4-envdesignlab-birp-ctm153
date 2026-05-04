mergeInto(LibraryManager.library, {
  GetURLParameter: function(name) {
    var nameStr = UTF8ToString(name);
    var url = new URL(window.location.href);
    var param = url.searchParams.get(nameStr) || "";
    var bufferSize = lengthBytesUTF8(param) + 1;
    var buffer = _malloc(bufferSize);
    stringToUTF8(param, buffer, bufferSize);
    return buffer;
  }
});
