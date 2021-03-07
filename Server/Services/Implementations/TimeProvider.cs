using System;

namespace Service.Server.Services.Implementations
{
    /// <summary>
    /// Determines the current time.
    /// </summary>
    public abstract class TimeProvider
    {
        // Default the time provider to provide real time.
        private static TimeProvider _current = new DefualtTimeProvider();

        /// <summary>
        /// Current time provider used by the application.
        /// </summary>
        public static TimeProvider Current
        {
            get
            {
                return _current;
            }
            set
            {
                if (value == null)
                {
                    throw new InvalidOperationException("Current time provider cannot be null.");
                }

                _current = value;
            }
        }

        /// <summary>
        /// Current date and time in native time zone.
        /// </summary>
        public abstract DateTime Now { get; }

        /// <summary>
        /// Current date and time in UTC.
        /// </summary>
        public abstract DateTime UtcNow { get;  }
    }

    /// <summary>
    /// Time provider used by default. Provides the machine's date and time.
    /// </summary>
    public class DefualtTimeProvider : TimeProvider
    {
        /// <summary>
        /// Current date and time in the machine's time zone.
        /// </summary>
        public override DateTime Now => DateTime.Now;

        /// <summary>
        /// Current date and time in UTC.
        /// </summary>
        public override DateTime UtcNow => DateTime.UtcNow;
    }
}
