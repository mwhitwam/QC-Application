with p as (
  SELECT [username]
    , 14 as [doom]
  FROM (
    VALUES 
      ('aownhouse'),
      ('fabrahams1'),
      ('hbraaf'),
      ('psamuels2'),
      ('wchordumn'),
      ('ygaidien')
  ) AS parameters([username])
)
, q as (
  SELECT o.queue_id
    , p.doom
  FROM queue_rel o
  JOIN p ON o.owner = p.username
  WHERE o.rel IN ('O', 'A')
)

select w.queue_id as [Queue ID]
  , w.strap as [Property ID]
  , q.doom - datediff(d, w.add_dt, getdate()) as [Days Remaining]
from wf_task w
join q
  on w.queue_id = q.queue_id 
where w.close_dt is null
  and w.queue_id LIKE 'SVC_LP%'
;