export function allowOnlyNumbers(event: KeyboardEvent): void {
  const allowedChars = /[0-9\s\-]/;
  const key = event.key;
  if (!allowedChars.test(key)) {
    event.preventDefault();
  }
}

export function allowOnlyLetters(event: KeyboardEvent): void {
  const allowedChars = /^[a-zA-ZÀ-ÿ\u00C0-\u017F\s'-]$/;
  const key = event.key;
  if (!allowedChars.test(key)) {
    event.preventDefault();
  }
}
