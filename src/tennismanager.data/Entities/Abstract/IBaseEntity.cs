﻿namespace tennismanager_api.tennismanager.data.Entities.Abstract;

public interface IBaseEntity<T>
{
    T Id { get; set; }
}