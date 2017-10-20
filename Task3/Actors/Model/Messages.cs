/*
Copyright 2017 Grigoryan Artem
Licensed under the Apache License, Version 2.0
*/

namespace Task3.Actors.Model
{
    public class Messages
    {
        #region Neutral/system messages

        public class ContinueProcessing { }

        #endregion

        #region Success messages

        public class InputSuccess
        {
            public InputSuccess(string reason)
            {
                Reason = reason;
            }

            public string Reason { get; private set; }
        }

        #endregion

        #region Error messages

        public class InputError
        {
            public InputError(string reason)
            {
                Reason = reason;
            }

            public string Reason { get; private set; }
        }

        public class NullInputError : InputError
        {
            public NullInputError(string reason) : base(reason) { }
        }

        public class ValidationError : InputError
        {
            public ValidationError(string reason) : base(reason) { }
        }

        #endregion
    }
}
