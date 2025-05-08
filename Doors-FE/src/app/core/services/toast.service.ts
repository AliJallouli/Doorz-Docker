import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class ToastService {
  show(message: string, type: 'success' | 'error' = 'success', duration: number = 4000, containerId: string = 'invitation-form-container') {
    const toast = document.createElement('div');
    toast.className = `custom-toast ${type}`;
    toast.innerText = message;

    Object.assign(toast.style, {
      position: 'absolute',
      top: '0px',
      left: '50%',
      transform: 'translateX(-50%)',
      backgroundColor: type === 'success' ? '#28a745' : '#dc3545',
      color: 'white',
      padding: '12px 24px',
      borderRadius: '8px',
      fontWeight: 'bold',
      zIndex: 1000,
      boxShadow: '0 4px 12px rgba(0,0,0,0.2)',
      opacity: '1',
      transition: 'opacity 0.5s ease'
    });

    const container = document.getElementById(containerId);
    if (container) {
      container.style.position = 'relative'; // important pour le positionnement
      container.appendChild(toast);
    } else {
      document.body.appendChild(toast); // fallback
    }

    setTimeout(() => {
      toast.style.opacity = '0';
      setTimeout(() => {
        toast.remove();
      }, 500);
    }, duration);
  }

}
