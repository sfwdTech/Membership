﻿namespace Membership.Blazor.ValueObjects;
internal class StateInfo
{
    public StateInfo(string state, string codeVerifier, string nonce, string scope, string returnUri)
    {
        State = state;
        CodeVerifier = codeVerifier;
        Nonce = nonce;
        Scope = scope;
        ReturnUri = returnUri;
    }

    public string State { get; }
    public string CodeVerifier { get; }
    public string Nonce { get; }
    public string Scope { get; }
    public string ReturnUri { get; }
}
