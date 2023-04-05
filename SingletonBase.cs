/*---------------------------------------------------------------------------------------------
 *  Copyright (c) 2023 - present handsomesnail
 *  Licensed under the MIT License. See LICENSE in the project root for more information.
 *--------------------------------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class SingletonBase<T> where T : class, new()
{
    private static volatile T instance;
    private static volatile bool allowInstantiated = false;
    private static readonly object lockObject = new object();

    protected SingletonBase()
    {
        if (!allowInstantiated || instance != null)
        {
            throw new InvalidOperationException();
        }
        instance = this as T;
    }

    public static T Instance
    {
        get
        {
            if (allowInstantiated)
            {
                throw new InvalidOperationException();
            }
            if (instance == null)
            {
                lock (lockObject)
                {
                    if (instance == null)
                    {
                        allowInstantiated = true;
                        try
                        {
                            instance = new T();
                        }
                        catch
                        {
                            allowInstantiated = false;
                            throw;
                        }
                        finally
                        {
                            allowInstantiated = false;
                        }
                    }
                }
            }
            return instance;
        }
    }
}
