drop table if exists public.project cascade;   -- remove your old table (deletes its rows)

create table public.projects (
  id          bigint generated always as identity primary key,
  title       text not null check (length(trim(title)) > 0),
  description text not null,
  icon        text not null default 'Sparkle',
  deleted_at  timestamptz,                      -- soft delete
  created_at  timestamptz not null default now()
);

create table public.tags (
  id   bigint generated always as identity primary key,
  name text not null unique
);

create table public.project_tags (
  project_id bigint not null references public.projects(id) on delete cascade,
  tag_id     bigint not null references public.tags(id) on delete cascade,
  primary key (project_id, tag_id)
);

create index on public.projects (title);        -- fast habit 1
create index on public.project_tags (tag_id);

-- One request returns each project with its tags (no fetch inside a loop)
create view public.project_cards with (security_invoker = true) as
select p.id, p.title, p.description, p.icon, p.deleted_at, p.created_at,
       coalesce(array_agg(t.name order by t.name) filter (where t.id is not null), '{}') as tags
from public.projects p
left join public.project_tags pt on pt.project_id = p.id
left join public.tags t on t.id = pt.tag_id
group by p.id;

-- Lock 1: grants
grant select, insert, update, delete on public.projects to anon;
grant select on public.tags, public.project_tags, public.project_cards to anon;

-- Lock 2: row level security (open policies are for class only)
alter table public.projects     enable row level security;
alter table public.tags         enable row level security;
alter table public.project_tags enable row level security;

create policy "read_all"   on public.projects for select to anon using (true);
create policy "insert_all" on public.projects for insert to anon with check (true);
create policy "update_all" on public.projects for update to anon using (true) with check (true);
create policy "delete_all" on public.projects for delete to anon using (true);
create policy "read_all"   on public.tags         for select to anon using (true);
create policy "read_all"   on public.project_tags for select to anon using (true);

-- Seed: your three current projects
insert into public.projects (title, description, icon) values
 ('Cozy Weather','A warm, minimal weather dashboard showing hourly forecasts, UV index, and mood-matched wallpapers.','Sun'),
 ('Pixel Garden','Procedurally generated pixel-art gardens. Grow plants, earn seeds, water neighbors'' plots in real time.','Sparkle'),
 ('Note Nook','A warm-toned note-taking app with rich text editing, tagging, and daily journal prompts.','Pin');

insert into public.tags (name) values
 ('Blazor WASM'),('C#'),('.NET 8'),('SignalR'),('HTML Canvas'),('Blazor Server'),('SQLite'),('EF Core');

insert into public.project_tags (project_id, tag_id)
select p.id, t.id
from (values ('Cozy Weather','Blazor WASM'),('Cozy Weather','C#'),('Cozy Weather','.NET 8'),
             ('Pixel Garden','C#'),('Pixel Garden','SignalR'),('Pixel Garden','HTML Canvas'),
             ('Note Nook','Blazor Server'),('Note Nook','SQLite'),('Note Nook','EF Core')) as v(ptitle, tname)
join public.projects p on p.title = v.ptitle
join public.tags t on t.name = v.tname;