create or replace view `View_HorarioMedicosDateWeek` as
select
	hm.HorarioMedicoId as 'HorarioMedicoId',
	hm.UsuarioId as 'UsuarioId',
	hm.DiaWeekId as 'DiaWeekId',
	hm.SucursalId as 'SucursalId',
	s.Nombre as 'SucursalName',
	th.ClinicaId as 'ClinicaId',
	th.Tipo_HorarioId as 'Tipo_HorarioId',	
	(cast(concat('2020-07-20 ', cast(th.Hora_Inicio as char(5))) as datetime) + interval hm.DiaWeekId day) as 'FechaInicio',
	(cast(concat('2020-07-20 ', cast(th.Hora_Fin as char(5))) as datetime) + interval (hm.DiaWeekId + (case	
																										when (th.Hora_Inicio > th.Hora_Fin) then 1 
																										else 0 end)) day) as 'FechaFin',
	((cast(concat('2020-07-20 ', cast(th.Hora_Inicio as char(5))) as datetime) + interval hm.DiaWeekId day) + interval 1 second) as 'Fecha_Inicio',
	((cast(concat('2020-07-20 ', cast(th.Hora_Fin as char(5))) as datetime) + interval (hm.DiaWeekId + (case	
																										when (th.Hora_Inicio > th.Hora_Fin) then 1 
																										else 0 end)) day) - interval 1 second) as 'Fecha_Fin'																									
from HorarioMedicos hm
inner join Tipo_Horarios th on hm.Tipo_HorarioId = th.Tipo_HorarioId
inner join Sucursals s on s.SucursalId = hm.SucursalId and s.ClinicaId = th.ClinicaId 