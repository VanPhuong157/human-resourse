class EventBus {
  constructor() {
    this.listeners = new Map()
  }

  // Register an event listener
  on(eventName, listener) {
    if (!this.listeners.has(eventName)) {
      this.listeners.set(eventName, [])
    }
    this.listeners.get(eventName).push(listener)
  }

  // Emit an event to all registered listeners
  emit(eventName, event) {
    const eventListeners = this.listeners.get(eventName)
    if (eventListeners) {
      eventListeners.forEach((listener) => listener(event))
    }
  }

  // Remove a specific listener for an event
  off(eventName, listener) {
    const eventListeners = this.listeners.get(eventName)
    if (eventListeners) {
      this.listeners.set(
        eventName,
        eventListeners.filter((l) => l !== listener),
      )
    }
  }

  // Remove all listeners for an event
  offAll(eventName) {
    this.listeners.delete(eventName)
  }
}

export const EVENT_BUS_KEY = {
  SELECTED_COLUMNS: 'SELECTED_COLUMNS',
  DATA_PERMISSIONS: 'DATA_PERMISSIONS',
}

const eventBus = new EventBus()
export default eventBus
