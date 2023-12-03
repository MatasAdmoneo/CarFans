export function removeLettersAfterNewline(inputString: string) {
  const pattern = /(.*?)\n/; // Match any characters (non-greedy) before the newline character '\n'
  const match = inputString.match(pattern);

  if (match) {
    return match[1]; // Return the matched substring before '\n'
  } else {
    return inputString; // Return the original string if no '\n' is found
  }
}
