using System;
using System.ComponentModel;

/// <summary>
/// http://www.clariusconsulting.net/blogs/kzu/archive/2008/03/10/58301.aspx
/// </summary>
[EditorBrowsable(EditorBrowsableState.Never)]
public interface IHideObjectMembers
{
    [EditorBrowsable(EditorBrowsableState.Never)]
    Type GetType();

    [EditorBrowsable(EditorBrowsableState.Never)]
    int GetHashCode();

    [EditorBrowsable(EditorBrowsableState.Never)]
    string ToString();

    [EditorBrowsable(EditorBrowsableState.Never)]
    bool Equals(object obj);
}

