function useDecode(encodedString) {
  // First decoding (URL decoding)
  const decodedOnce = decodeURIComponent(encodedString);

  // Second decoding (character entity decoding)
  const decoder = new TextDecoder();
  const decodedTwice = decoder.decode(
    new Uint8Array(decodedOnce.split("").map((char) => char.charCodeAt(0)))
  );

  return decodedTwice;
}

export default useDecode;
