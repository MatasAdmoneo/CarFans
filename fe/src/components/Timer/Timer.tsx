"use client"
import { useEffect } from 'react';
import { useTimer } from 'react-timer-hook';

export default function Timer({ expiryTimestamp }: { expiryTimestamp: Date }) {
  const {
    seconds,
    minutes,
    hours,
    days,
    isRunning,
    start
  } = useTimer({
    expiryTimestamp: new Date(expiryTimestamp),
    autoStart: false
  });

  useEffect(() => {
    if (!isRunning) {
      start();
    }
  }, [isRunning, start])

  return (
    <div style={{textAlign: 'center'}}>
      {isRunning && (
        <div>
          <span>{String(days).padStart(2, "0")}</span>:
          <span>{String(hours).padStart(2, "0")}</span>:
          <span>{String(minutes).padStart(2, "0")}</span>:
          <span>{String(seconds).padStart(2, "0")}</span>
        </div>
      )}
    </div>
  );
}