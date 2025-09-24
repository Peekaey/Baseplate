import { clsx, type ClassValue } from 'clsx'
import { twMerge } from 'tailwind-merge'

export function cn(...inputs: ClassValue[]) {
  return twMerge(clsx(inputs))
}

export function ConvertDateToLocalString(date: Date): string {
    
    if (!date instanceof Date) {
        return "Unknown Date";
    } else {
        const convertedDate = new Date(date).toLocaleDateString();
        const localDate = convertedDate.toLocaleString();
        return localDate;
    }
}