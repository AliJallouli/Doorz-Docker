// src/app/core/errors/error-context-resolver.ts
import { errorContextMap } from './error-context.map';

/**
 * Construit la clé de traduction complète à partir d'une erreur backend.
 * @param key Clé d'erreur renvoyée par le backend
 * @returns Clé de traduction ngx-translate (ex: 'LOGIN.ERROR.BACKEND.INVALID_LOGIN')
 */
export function resolveErrorTranslationKey(key: string): string {
  const prefix = errorContextMap[key] || 'SHARED.ERROR.BACKEND';
  return `${prefix}.${key}`;
}
